using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SetValueTexture : MonoBehaviour
{
    public RenderTexture texture;
    public Slider slider;

    private void Fullbleed(RenderTexture tex, float val)
    {
        Texture2D texture = new Texture2D(tex.width, tex.height);
        Texture2D t = tex.toTexture2D();

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++)
            {
                Color a = t.GetPixel(x, y);
                Color color = new Color(val, val, val, 1);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();

        Graphics.Blit(texture, tex);
    }

    public void Run()
    {
        Fullbleed(texture, slider.value);
    }
}
