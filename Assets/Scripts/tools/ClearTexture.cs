using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearTexture : MonoBehaviour
{
    public RenderTexture renderTexture; 
    public void Clear() 
    {
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, renderTexture.width, renderTexture.height, 0);

        RenderTexture.active = renderTexture;

        Texture2D tex = new Texture2D(1, 1);

        tex.SetPixel(0, 0, new Color(0, 0, 0, 1));

        tex.Apply();

        Graphics.DrawTexture(new Rect(0, 0, renderTexture.width, renderTexture.height), tex);

        RenderTexture.active = null;

        GL.PopMatrix();
    }
}
