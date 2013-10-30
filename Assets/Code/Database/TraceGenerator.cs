using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TraceGenerator {

    public static List<Triple<double, string, string>> GenerateRandomTrace(List<float> times, float minTime, float maxTime, List<string> objects, List<List<Pair<string, int>>> objectTagCounts) {
        List<Triple<double, string, string>> trace = new List<Triple<double, string, string>>();

        // Error checking
        if (times.Count != objects.Count) {
            return null;
        }

        // XXX (kasiu): Consider permuting the times. We don't actually do it here.
        // Right now, just sorts the times to get them together.
        times.Sort();

        // Permute the object list.
        List<string> permutedObjects = GenerateRandomPermutation(objects);

        for (int i = 0; i < times.Count; i++) {
            string obj = permutedObjects[i];

            int oldIndex = -1;
            for (int j = 0; j < objects.Count; j++) {
                if (obj == objects[j]) {
                    oldIndex = j;
                    break;
                }
            }

            string tag = GenerateRandomTag(objectTagCounts[oldIndex]);
            Triple<double, string, string> traceEntry = new Triple<double, string, string>(Mathf.Clamp(times[i], minTime, maxTime), obj, tag);
        }
       
        return trace;
    }

    /// <summary></summary>
    /// <param name="list">List of strings</param>
    /// <returns>A list of randomly permuted input strings</returns>
    private static List<string> GenerateRandomPermutation(List<string> list) {
        List<string> permutedList = new List<string>(list);
        // Stolen from a friend who stole it from StackOverflow :P
        for (int i = list.Count - 1; i >= 0; i--) {
            int n = Random.Range(0, i + 1);
            string item = permutedList[i];
            permutedList[i] = permutedList[n];
            permutedList[n] = item;            
        }
        return permutedList;
    }

    /// <summary>
    /// Generates a random tag based on the pairings of tag/count.
    /// Utilizes probability based on the count total.
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    private static string GenerateRandomTag(List<Pair<string, int>> list) {
        int total = 0;
        foreach (Pair<string, int> p in list) {
            total += p.Second;
        }

        // Is it better to use MS's random instead of Unity's? Whatever.
        int random = Random.Range(0, total);

        // Think of the following as running 
        int runningTotal = 0;
        string tag = null;
        foreach (Pair<string, int> p in list) {
            if (runningTotal + p.Second > random) {
                tag = p.First;
                break;
            }
            runningTotal += p.Second;
        }
        return tag;
    }
}
