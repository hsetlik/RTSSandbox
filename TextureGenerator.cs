using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator 
{
  public static Texture2D TextureFromColorMap(Color[] map, int width, int height)
    {
        Texture2D tex = new Texture2D(width, height);
        tex.filterMode = FilterMode.Point;
        tex.wrapMode = TextureWrapMode.Clamp;
        tex.SetPixels(map);
        tex.Apply();
        return tex;
    }
    public static Texture2D TextureFromHeightMap(float[,] map)
    {
        int width = map.GetLength(0);
        int height = map.GetLength(1);

        Texture2D texture = new Texture2D(width, height);

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; ++y)
        {
            for (int x = 0; x < width; ++x)
            {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, map[x, y]);
            }
        }
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }
        
}
