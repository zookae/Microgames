using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DBStringHelper {

    public static string traceToString(List<Triple<double, string, string>> trace, char delimeter) {
        string times = "";
        string objects = "";
        string tags = "";

        for (int i = 0; i < trace.Count; i++) {
            Triple<double, string, string> t = trace[i];
            times += t.First;
            objects += t.Second;
            tags += t.Third;

            if (i != trace.Count - 1) {
                times += ",";
                objects += ",";
                tags += ",";
            }
        }

        // Concantenates everything into one large string.
        // Whoever needs it can take care of parsing it.
        // XXX (kasiu): This is bad. Also, if the delimeter is a comma, things will break :P
        return times + delimeter + objects + delimeter + tags;
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
}
