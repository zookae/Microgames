using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// A class for loading a text/texture mapping.
/// Made static so that is doesn't need to be attached as a script.
/// LoadObject invokes this in order to load the textures.
/// </summary>
public static class TextToTextureLoader{

    private static TextAsset objectSpriteMapping;
    private static string directoryHeader;
    private static Dictionary<string, Texture> itemImageMap = new Dictionary<string, Texture>();

    public static Texture RetrieveTexture(string s) {
        if (objectSpriteMapping == null) {
            return null;
        }
        return itemImageMap[s];
    }

    public static void SetTextTexureMapping(TextAsset ta, string header) {
        if (objectSpriteMapping == null) {
            objectSpriteMapping = ta;
            directoryHeader = header;
            LoadMapping();
        }
    }

    private static void LoadMapping() {
        string[] itemList = objectSpriteMapping.text.Split('\n');
        foreach (string s in itemList) {
            string[] pair = s.Split(',');
            if (pair.Length == 2) {
                string textureName = (directoryHeader + pair[1]).Trim() ;
                Texture texture = Resources.Load(textureName) as Texture;
                if (texture != null) {
                    DebugConsole.Log("For item " + pair[0] + " we're loading: " + textureName);
                    itemImageMap.Add(pair[0], texture);
                }
            }
        }
    }
 }

