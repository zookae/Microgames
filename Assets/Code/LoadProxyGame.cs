using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoadProxyGame : MonoBehaviour {
    /// <summary>
    /// The possible prefabs that can be spawned
    /// </summary>
    public GameObject[] prefabs;

    /// <summary>
    /// HACK HACK HACK HACK HACK (kasiu): Replace with a load from the DB or something.
    /// </summary>
    public TextAsset objectFile;

    /// <summary>
    /// The number of objects for the game.
    /// </summary>
    public int numObjects;

    /// <summary>
    /// The list of all possible objects.
    /// </summary>
    public List<string> allObjects;

    /// <summary>
    /// The list of all possible tags.
    /// </summary>
    /// HACK (kasiu): Hacked in for simplicity currently. Eventually load from file/DB.
    public List<string> allTags = new List<string> { "Pharmacy", "Supermarket", "Department Store", "Hardware Store" };

	// Use this for initialization
	void Start () {
        // XXX (kasiu): Remove eventually.
        LoadTemporaryTextAsset();

        ScoringMode mode = SelectRandomScoringMode();
        List<string> objectSet = SelectRandomSubset(numObjects, allObjects);
        List<string> tagSet = SelectRandomSubset(2, allTags);

        // HACK (kasiu):
        if (mode == ScoringMode.Both) {
            tagSet.Add(tagSet[tagSet.Count - 1]);
        }

        // Set things.
        GameState.Singleton.ScoringMode = mode;

        // Modify the spawner
        GameObject spawner = GameObject.Find("Spawner");
        if (spawner != null && (spawner.GetComponent<LoadObject>() != null)) {
            if (mode == ScoringMode.Both) {
                spawner.GetComponent<LoadObject>().prefab = prefabs[1];
            } else {
                spawner.GetComponent<LoadObject>().prefab = prefabs[0];
            }

            spawner.GetComponent<LoadObject>().objectNames = objectSet;
            spawner.GetComponent<LoadObject>().tagNames = tagSet;
        }
	}
	
	// Update is called once per frame
	void Update () {}

    // XXX (kasiu): Currently using the same item-texture mapping file. CHANGE EVENTUALLY PLEASE.
    private void LoadTemporaryTextAsset() {
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

    /// <summary>
    /// Selects a random scoring mode.
    /// </summary>
    /// <returns>A random scoring mode</returns>
    public ScoringMode SelectRandomScoringMode() {
        ScoringMode[] modes = (ScoringMode[])System.Enum.GetValues(typeof(ScoringMode));
        int index = Random.Range(0, modes.Length);
        return modes[index];
    }

    /// <summary>
    /// Selects a random subset of strings from a list of strings. No duplicates.
    /// </summary>
    /// <param name="numItems">Number of items in the subset</param>
    /// <param name="fullSet">The full set of strings</param>
    /// <returns>A sublist of strings or an empty list if set contains fewer strings than requested</returns>
    public List<string> SelectRandomSubset(int numItems, List<string> fullSet) {
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