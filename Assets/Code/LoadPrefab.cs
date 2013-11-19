using UnityEngine;
using System.Collections;
using UnityEditor;

public class LoadPrefab : MonoBehaviour {

    /// <summary>
    /// Gets the source path to a prefab if any.
    /// </summary>
    /// <param name="prefab">The prefab reference to get the asset path for.</param>
    /// <returns>Returns a asset path for a prefab.</returns>
    /// <remarks>This method will attempt to find the source asset of the given <see cref="UnityEngine.Object"/> by
    /// walking up the parent prefab hierarchy.</remarks>
    public static string GetSourcePrefab(UnityEngine.Object prefab) {
        // if no prefab specified then return null
        if (prefab == null) {
            return null;
        }

        // attempt to get the path
        var path = AssetDatabase.GetAssetPath(prefab);
        // if no path returned it may be an instantiated prefab so try to get the parent prefab
        while (System.String.IsNullOrEmpty(path)) {
            // try parent prefab
            var parent = PrefabUtility.GetPrefabParent(prefab);

            // no parent so must be generated through code so just exit loop
            if (parent == null) {
                break;
            }

            // attempt to get path for
            path = AssetDatabase.GetAssetPath(parent);

            // set prefab reference to parent for next loop
            prefab = parent;
        }

        // return the path if any
        return path;
    }

    public static Object GetPrefabFromString(string prefabName) {
        Object go = AssetDatabase.LoadMainAssetAtPath(prefabName);
        return go;
    }

    public GameObject prefabtest;

    void Start() {
        //Debug.Log("loading test prefab from: " + GetSourcePrefab(prefabtest));
    }
}
