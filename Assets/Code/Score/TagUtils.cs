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

    public static Vector3 GetPositionOfChildTag(GameObject obj, string childName) {
        if (obj == null) {
            // XXX (kasiu): We should throw an exception here.
            return Vector3.zero;
        } else if (obj.transform.FindChild(childName) == null) {
            return obj.transform.position;
        }
        return obj.transform.FindChild(childName).transform.position;
    }
}