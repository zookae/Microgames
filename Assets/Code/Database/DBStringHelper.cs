using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DBStringHelper {

    // We format the string as follows: "x1:x2:x_n,y1:y2:y_n,z1:z2:z_n"
    // where x's are times, y's are objects, and z's are tags.
    // The client is going to split on commas
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
                times += delimeter;
                objects += delimeter;
                tags += delimeter;
            }
        }

        // Concantenates everything into one large string.
        // Whoever needs it can take care of parsing it using the below method.
        // XXX (kasiu): This is bad. Also, if the delimeter is a comma, things will break :P
        return times + ',' + objects + ',' + tags;
    }

    // Due to the way that the server/client auto-disassembles strings, we format the list
    // as follows: "x1:x2:x3,y1:y2:y3,z1:z2:z3" where each of x, y, z are now their own string
    public static List<Triple<double, string, string>> stringToTrace(string[] args, char delimeter) {
        if (args.Length != 3) {
            // BAD FORMATTING
            return null;
        }

        string[][] parsedArgs = new string[3][];
        for (int i = 0; i < args.Length; i++) {
            parsedArgs[i] = (args[i]).Split(delimeter);
        }

        if (parsedArgs[0].Length != parsedArgs[1].Length || parsedArgs[0].Length != parsedArgs[2].Length) {
            // MORE BAD FORMATTING
            return null;
        }
 
        List<Triple<double, string, string>> trace = new List<Triple<double, string, string>>();
        for (int i = 0; i < parsedArgs[0].Length; i++) {
            Triple<double, string, string> triple = new Triple<double, string, string>();
            triple.First = System.Convert.ToDouble(parsedArgs[0][i]);
            triple.Second = parsedArgs[1][i];
            triple.Third = parsedArgs[2][i];
            trace.Add(triple);
        }
        return trace;
    }

    public static string listToString(List<string> list, char delimeter) {
        string s = "";
        for (int i = 0; i < list.Count; i++) {
            s += list[i];

            if (i != list.Count - 1) {
                s += delimeter;
            }
        }
        return s;
    }

    public static List<string> stringToList(string s, char delimeter) {
        string[] arr = s.Split(delimeter);
        return new List<string>(arr);
    }
}
