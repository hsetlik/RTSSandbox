using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode
    {
        NoiseMap, ColorMap, DrawMesh
    };
    public DrawMode currentMode;
    public int MapWidth;
    public int MapHeight;
    public float NoiseScale;
    public int Octaves;
    public float Persistence;
    public float Lacunarity;
    public bool AutoUpdate;
    public int seed;
    public Vector2 offset;
    public TerrainType[] regions;
    public float MeshHeightGain;
    public void Start()
    {
        GenerateMap();
    }
    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(MapWidth, MapHeight, NoiseScale, Octaves, Persistence, Lacunarity, seed, offset);
        Color[] colorMap = new Color[MapWidth * MapHeight];
        for(int y = 0; y < MapHeight; ++y)
        {
            for(int x = 0; x < MapWidth; ++x)
            {
                float currentHeight = noiseMap[x, y];
                for(int i = 0; i < regions.Length; ++i)
                {
                    if(currentHeight <= regions[i].height)
                    {
                        colorMap[y * MapWidth + x] = regions[i].color;
                        break;
                    }
                }
            }
        }
        MapDisplay mDisplay = FindObjectOfType<MapDisplay>();
        if (currentMode == DrawMode.NoiseMap)
            mDisplay.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        else if (currentMode == DrawMode.ColorMap)
            mDisplay.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, MapWidth, MapHeight));
        else if (currentMode == DrawMode.DrawMesh)
            mDisplay.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, MeshHeightGain), TextureGenerator.TextureFromColorMap(colorMap, MapWidth, MapHeight));
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
}

