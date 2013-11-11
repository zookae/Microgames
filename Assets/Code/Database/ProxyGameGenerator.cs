using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class ProxyGameGenerator
{

    /// <summary>
    /// Selects a random scoring mode.
    /// </summary>
    /// <returns>A random scoring mode</returns>
    public static int SelectRandomScoringMode() {
        return Random.Range(1, 4);
    }

    public static List<string> SelectRandomObjectSet(int numObjects) {
        return DBGWAPLoader.GenerateRandomObjectSet(7);
    }

    public static List<string> SelectRandomTagSet(ScoringMode scoringMode) {
        List<string> tagSet = DBGWAPLoader.GenerateRandomTagset();
        if (scoringMode == ScoringMode.Both) {
            tagSet.Add(tagSet[tagSet.Count - 1]);
        }
        return tagSet;
    }

    public static List<Triple<double, string, string>> SelectRandomPartnerTrace(List<string> objects, List<string> tags, float minTime, float maxTime) {
        List<Triple<double, string, string>> list = new List<Triple<double, string, string>>();
        int numTimes = Random.Range(1, objects.Count + 1);

        // HACK (kasiu): Doesn't guarantee a good time distribution.
        List<double> times = new List<double>();
        for (int i = 0; i < numTimes; i++) {
            times.Add(Random.Range(minTime, maxTime));
        }
        times.Sort();
        List<string> objectSubset = SelectRandomSubset(numTimes, objects);

        for (int i = 0; i < numTimes; i++) {
            Triple<double, string, string> triple = new Triple<double, string, string>();
            triple.First = times[i];
            triple.Second = objectSubset[i];
            int randomTag = Random.Range(0, 2);
            triple.Third = tags[randomTag];

            list.Add(triple);
        }        

        return list;
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