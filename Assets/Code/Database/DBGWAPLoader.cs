using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// The REAL loader (borrows from LoadProxyGame)
public static class DBGWAPLoader {
    /// <summary>
    /// HACK HACK HACK HACK HACK (kasiu): Replace with a load from the DB or something.
    /// </summary>
    private static TextAsset objectFile = null;

    /// <summary>
    /// The list of all possible tags.
    /// </summary>
    /// HACK (kasiu): Hacked in for simplicity currently. Eventually load from file/DB.
    private static List<string> allTags = new List<string> { "Pharmacy", "Supermarket", "Department Store", "Hardware Store" };

    /// <summary>
    /// The list of all possible objects.
    /// </summary>
    public static List<string> allObjects;

    public static List<string> GenerateRandomObjectSet(int numObjects) {
        // Load the file if it hasn't been loaded.
        // HACK (kasiu): Again, the load is a little hacky. But we'll deal with it later.
        if (objectFile == null) { // then allObjects is empty
            allObjects = new List<string>();
            objectFile = Resources.Load("itemImageMap") as TextAsset;
            if (objectFile != null) {
                string[] itemList = objectFile.text.Split('\n');
                foreach (string s in itemList) {
                    string[] pair = s.Split(',');
                    if (pair.Length >= 1) {
                        allObjects.Add(pair[0]);
                    }
                }
            }
        }
        if (allObjects.Count > 0) {
            return SelectRandomSubset(numObjects, allObjects);
        }
        return null;
    }

    public static List<string> GenerateRandomTagset() {
        return SelectRandomSubset(2, allTags);
    }

    public static List<Triple<double, string, string>> ConstructRandomPartnerTrace(string[] times, List<string> objects, List<string> tags) {
        int numTimes = times.Length;
        List<string> objectSubset = (objects.Count == numTimes) ? objects : SelectRandomSubset(numTimes, objects);

        List<Triple<double, string, string>> partnerTrace = new List<Triple<double, string, string>>();
        for (int t = 0; t < numTimes; t++) {
            Triple<double, string, string> triple = new Triple<double, string, string>();
            triple.First = System.Convert.ToDouble(times[0]);
            triple.Second = objectSubset[t];
            triple.Third = SelectRandomSubset(1, tags)[0];
            partnerTrace.Add(triple);
            DebugConsole.Log("Adding trace element of : " + triple.First + ", " + triple.Second + ", " + triple.Third);
        }
        return partnerTrace;
    }

    /// <summary>
    /// Selects a random subset of strings from a list of strings. No duplicates.
    /// </summary>
    /// <param name="numItems">Number of items in the subset</param>
    /// <param name="fullSet">The full set of strings</param>
    /// <returns>A sublist of strings or an empty list if set contains fewer strings than requested</returns>
    private static List<string> SelectRandomSubset(int numItems, List<string> fullSet) {
        List<string> set = new List<string>();

        if (numItems > fullSet.Count) {
            // XXX (kasiu): Should probably throw an exception.
            return set;
        } else if (numItems == fullSet.Count) {
            return fullSet; // :P
        }

        while (set.Count < numItems) {
            int index = Random.Range(0, fullSet.Count);
            string randomObject = fullSet[index];
            if (!set.Contains(randomObject)) {
                set.Add(randomObject);
            }
        }
        return set;
    }
}
