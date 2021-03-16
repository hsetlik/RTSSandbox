using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    public Renderer rTexture;
    public MeshFilter mFilter;
    public MeshRenderer mRender;
    public void DrawTexture(Texture2D texture)
    {
        rTexture.sharedMaterial.mainTexture = texture;
        rTexture.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }

    public void DrawMesh(MeshData mData, Texture2D texture)
    {
        mFilter.sharedMesh = mData.CreateMesh();
        mRender.sharedMaterial.mainTexture = texture;
    }
}
