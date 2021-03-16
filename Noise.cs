using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, int octaves, float persistence, float lacunarity, int seed, Vector2 offset)
    {

        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];
        for(int i = 0; i < octaves; ++i)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }
        float halfWidth = mapWidth / 2.0f;
        float halfHeight = mapHeight / 2.0f;
        if (scale <= 0.0f)
            scale = 0.0001f; //avoid divide-by-zero error
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;
        float[,] noiseMap = new float[mapWidth, mapHeight];
        for(int y = 0; y < mapHeight; ++y)
        {
            for(int x = 0; x < mapWidth; ++x)
            {
                float amplitude = 1.0f;
                float frequency = 1.0f;
                float noiseHeight = 0;

                for(int i = 0; i < octaves; ++i)
                {
                    float sampleX = (x - halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y - halfHeight) / scale * frequency + octaveOffsets[i].y;

                    float pValue = (Mathf.PerlinNoise(sampleX, sampleY) * 2) - 1;
                    noiseHeight += pValue * amplitude;
                    amplitude *= persistence;
                    frequency *= lacunarity;
                    
                }
                noiseMap[x, y] = noiseHeight;
                if (noiseHeight > maxNoiseHeight)
                    maxNoiseHeight = noiseHeight;
                if (noiseHeight < minNoiseHeight)
                    minNoiseHeight = noiseHeight;
            }
        }
        for (int y = 0; y < mapHeight; ++y)
        {
            for (int x = 0; x < mapWidth; ++x)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }
        return noiseMap;
    }
}
