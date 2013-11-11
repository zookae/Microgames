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
}
