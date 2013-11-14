using UnityEngine;
using System.Collections;

public static class TagUtils
{
    public static string TrimBothModeTag(string tag) {
        if (tag.Contains("-collab")) {
            int index = tag.IndexOf('-');
            return tag.Substring(0, index);
        } else if (tag.Contains("-compete")) {
            int index = tag.IndexOf('-');
            return tag.Substring(0, index);
        }

        return tag;
    }
}