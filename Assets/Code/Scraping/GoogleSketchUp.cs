using UnityEngine;
using System.Collections;
using System.Net;
using System.Diagnostics;
using HtmlAgilityPack;

// the main implementation of these are courtesy of Michael Cook: http://www.gamesbyangelina.org/2013/07/sketchup-3d-warehouse-and-unity/
public class GoogleSketchUp  {

    // acquiring HtmlAgilityPack:
    // (1) install NuGet: http://docs.nuget.org/docs/start-here/using-the-package-manager-console
    // (2) located at: https://www.nuget.org/packages/HtmlAgilityPack
    //      instructions specfiy to use package manager console with command "Install-Package HtmlAgilityPack"
    // (3) copy the .Net 2.0 (Unity compatibility version) dll file
    //      from:   \packages\HtmlAgilityPack.1.4.6\lib\Net20
    //      to:     \Assets\Code\Scraping (or wherever this script is located
    // (4) add reference to the .dll (this will not work with the default location)
    //      Project -> Add Reference... -> (wherever you copied dll file to)
    //  note: any updates to HtmlAgilityPack will require this change

    /// <summary>
    /// Search for a term on Google Sketch-Up and download the first resulting model
    /// </summary>
    /// <param name="searchterm">Term to search for on Sketch-Up for models</param>
    /// <param name="downloadPath">Path to store the results at</param>
    /// <param name="defaultDownloadPath">Path the computer downloads to by default</param>
    /// <returns></returns>
    public static IEnumerator aSyncSearchForModel(string searchterm, string downloadPath, string defaultDownloadPath) {
        WebClient w = new WebClient();
        string htmlstring = w.DownloadString("http://sketchup.google.com/3dwarehouse/doadvsearch?title=" + searchterm
                                    + "&scoring=d&btnG=Search+3D+Warehouse&dscr=&tags=&styp=m&complexity=any_value&file="
                                    + "zip" + "&stars=any_value&nickname=&createtime=any_value&modtime=any_value&isgeo=any_value&addr=&clid=");

        // must install via NuGet: http://docs.nuget.org/docs/start-here/using-the-package-manager-console
        // located at: https://www.nuget.org/packages/HtmlAgilityPack
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(htmlstring);

        //Grab a result from the search page
        HtmlNodeCollection results = doc.DocumentNode.SelectNodes("//div[@class='searchresult']");
        string model_page_url = "http://sketchup.google.com" + results[0].SelectSingleNode(".//a").Attributes["href"].Value;

        htmlstring = w.DownloadString(model_page_url);
        doc.LoadHtml(htmlstring);
        UnityEngine.Debug.Log(htmlstring);

        //Find the downloadable zip file
        results = doc.DocumentNode.SelectNodes("//a[contains(@href,'rtyp=zip')]");

        string url = "http://sketchup.google.com" + WWW.UnEscapeURL(results[0].Attributes["href"].Value);
        url = url.Replace("&amp;", "&");
        UnityEngine.Debug.Log(WWW.EscapeURL(results[0].Attributes["href"].Value));
        UnityEngine.Debug.Log(url);

        WWW model_zip = new WWW(url);
        yield return model_zip;

        string file_loc = downloadPath + "/" + searchterm + ".zip";
        UnityEngine.Debug.Log("file at: " + file_loc);
        System.IO.FileStream filestream = new System.IO.FileStream(file_loc, System.IO.FileMode.Create, System.IO.FileAccess.Write); //Assets/Resources/sfx/
        filestream.Write(model_zip.bytes, 0, model_zip.bytes.Length);
        filestream.Close();

    }


    // adapted from Michael Cook: http://www.gamesbyangelina.org/2013/07/sketchup-3d-warehouse-and-unity/
    /// <summary>
    /// Extract all files from archived data using 7zip
    /// note: 7z command must be on the path
    /// </summary>
    /// <param name="filename">Name of file to unzip</param>
    /// <param name="dest">Folder to store unzipped file in</param>
    public static void unzip(string filename, string dest) {
        string args = " x " + filename + " o" + dest + "-y"; // assume "yes" to all questions it asks

        UnityEngine.Debug.Log("unzipping: " + filename + " to: " + dest);

        Process p = new Process();
        p.StartInfo.FileName = "7z";
        p.StartInfo.Arguments = args;
        p.Start();
        p.WaitForExit();
    }


    private static void ConvertFromDaeToFbx(string filename, string searchterm) {
        string fbx_path = "%FBX%\\FbxConverter.exe";
        string args = filename + " " + searchterm + ".fbx";

        Process p = new Process();
        p.StartInfo.FileName = fbx_path;
        p.StartInfo.Arguments = args;
        p.Start();
        p.WaitForExit();
    }
}
