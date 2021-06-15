using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearStart : MonoBehaviour
{
    public RenderTexture[] renderTexture; 

    private void Clear(RenderTexture texture) 
    {
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, texture.width, texture.height, 0);

        RenderTexture.active = texture;

        Texture2D tex = new Texture2D(1, 1);

        tex.SetPixel(0, 0, new Color(0, 0, 0, 1));

        tex.Apply();

        Graphics.DrawTexture(new Rect(0, 0, texture.width, texture.height), tex);

        RenderTexture.active = null;

        GL.PopMatrix();
    }

    private void ClearAll()
    {
        foreach (RenderTexture val in renderTexture) Clear(val);
    }

    void Start()
    {
        ClearAll();
    }
}
