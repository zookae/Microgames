using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DBStringHelper {

    public static string traceToString(List<Triple<double, string, string>> trace, char delimeter) {
        // HACK (kasiu): So ashamed of writing stupid code like this. Too tired to care.
        if (delimeter == ',') {
            return null;
        }

        string times = "";
        string objects = "";
        string tags = "";

        for (int i = 0; i < trace.Count; i++) {
            Triple<double, string, string> t = trace[i];
            times += t.First.ToString();
            objects += t.Second.ToString();
            tags += t.Third.ToString();

            // HACK (kasiu): Assumes commas.
            if (i != trace.Count - 1) {
                times += ",";
                objects += ",";
                tags += ",";
            }
        }

        // Concantenates everything into one large string.
        // Whoever needs it can take care of parsing it using the below method.
        // XXX (kasiu): This is bad. Also, if the delimeter is a comma, things will break :P
        return times + delimeter + objects + delimeter + tags;
    }

    public static List<Triple<double, string, string>> stringToTrace(string s, char delimeter) {
        List<Triple<double, string, string>> trace = new List<Triple<double, string, string>>();
        string[] args = s.Split(delimeter);
        if (args.Length != 3) {
            // BAD FORMATTING
            return null;
        }

        string[] times = args[0].Split(',');
        string[] objects = args[1].Split(',');
        string[] tags = args[2].Split(',');

        if ((times.Length != objects.Length) || (times.Length != tags.Length)) {
            // MORE BAD FORMATTING
            return null;
        }

        for (int i = 0; i < times.Length; i++) {
            Triple<double, string, string> triple = new Triple<double, string, string>();
            triple.First = System.Convert.ToDouble(times[i]);
            triple.Second = objects[i];
            triple.Third = tags[i];
            trace.Add(triple);
        }

        return trace;
    }

    public static string listToString(List<string> list) {
        string s = "";
        for (int i = 0; i < list.Count; i++) {
            s += list[i];

            if (i != list.Count - 1) {
                s += ",";
            }
        }
        return s;
    }

    public static List<string> stringToList(string s, char delimeter) {
        string[] arr = s.Split(delimeter);
        return new List<string>(arr);
    }
}
