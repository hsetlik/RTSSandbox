using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{
    public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve curve)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        float topLeftX = (width - 1) / -2.0f;
        float topLeftZ = (height - 1) / 2.0f;
        int vertexIndex = 0;
        MeshData mData = new MeshData(width, height);
        for(int y = 0; y < height; ++y)
        {
            for(int x = 0; x < width; ++x)
            {
                mData.vertices[vertexIndex] = new Vector3(topLeftX + x, curve.Evaluate(heightMap[x, y]) * heightMultiplier, topLeftZ - y);
                mData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);
                if(x < (width - 1) && y < (height - 1))
                {
                    mData.AddTriangle(vertexIndex, vertexIndex + width + 1, vertexIndex + width);
                    mData.AddTriangle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
                }
                vertexIndex++;
            }
                
        }
        return mData;
    }
}

public class MeshData
{
    public Vector3[] vertices;
    public Vector2[] uvs;
    public int[] triangles;
    int triangleIndex;
    public MeshData(int mWidth, int mHeight)
    {
        triangleIndex = 0;
        vertices = new Vector3[mWidth * mHeight];
        uvs = new Vector2[mWidth * mHeight];
        triangles = new int[(mWidth - 1) * (mHeight - 1) * 6];

    }
    public void AddTriangle(int a, int b, int c)
    {
        triangles[triangleIndex] = a;
        triangles[triangleIndex + 1] = b;
        triangles[triangleIndex + 2] = c;
        triangleIndex += 3;
    }

    public Mesh CreateMesh()
    {
        Mesh output = new Mesh();
        output.vertices = vertices;
        output.triangles = triangles;
        output.uv = uvs;
        output.RecalculateNormals();
        return output;
    }
}

