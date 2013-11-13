using UnityEngine;
using System.Collections;

public static class GUIUtils {
    // Generates a blank texture.
    // http://forum.unity3d.com/threads/66015-Changing-the-Background-Color-for-BeginHorizontal
    public static Texture2D MakeBlankTexture(int width, int height, Color col) {
        Color[] pixels = new Color[width * height];

        for (int i = 0; i < pixels.Length; i++) {
            pixels[i] = col;
        }
        Texture2D tex = new Texture2D(width, height);
        tex.SetPixels(pixels);
        tex.Apply();

        return tex;
    }

    /// <summary>
    /// Helper to compute the position for a centered box.
    /// </summary>
    /// <param name="width"></param>
    /// <param name="height"></param>
    /// <returns>The screen space position for a box.</returns>
    public static Vector2 ComputeCenteredPosition(int width, int height) {
        return new Vector2((Screen.width - width) / 2.0f, (Screen.height - height) / 2.0f);
    }

    public static void SpawnFloatingText(Vector2 position, Vector2 velocity, string text) {
        GameObject obj = new GameObject();
        //obj.AddComponent<>();
        //obj
    }
}
