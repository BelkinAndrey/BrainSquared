using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Filling : MonoBehaviour
{
    public RenderTexture texture;

    void Start()
    {
        FullRandom(texture);  
    }

    public void SetRandom()
    {
        FullRandom(texture);   
    }

    private void FullRandom(RenderTexture tex)
    {
        Texture2D texture = new Texture2D(tex.width, tex.height);
        Texture2D t = tex.toTexture2D();

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color a = t.GetPixel(x, y);
                Color color = new Color(Random.Range(0, 1f), 0, 0, 1);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();

        Graphics.Blit(texture, tex);
    }
}
