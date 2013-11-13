using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// A gold standard hack.
public static class ScoreStandardDictionary
{
    private static TextAsset text = Resources.Load("itemGoldStandard") as TextAsset;
    private static Dictionary<string, List<string>> dictionary = null;

    private static void BuildDictionary() {
        if (dictionary != null) {
            return;
        }

        // Otherwise build it
        dictionary = new Dictionary<string, List<string>>();
        string[] items = text.text.Split('\n');
        for (int i = 0; i < items.Length; i++) {
            string[] components = items[i].Split(',');
            string obj = components[0];
            List<string> tags = new List<string>();
            for (int j = 1; j < components.Length; j++) {
                // ALWAYS TRIM YOUR STRINGS
                tags.Add(components[j].Trim());
            }
            dictionary.Add(obj, tags);
        }
    }


    public static bool MatchesStandard(string obj, string tag) {
        if (dictionary == null) {
            BuildDictionary();
        }

        return (dictionary[obj]).Contains(tag);
    }
}