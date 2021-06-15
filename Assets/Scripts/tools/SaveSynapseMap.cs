using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveSynapseMap : MonoBehaviour
{
    public RenderTexture renderTexture;
    public string nameTexture;

    public void Save()
    {
        SavePNG(renderTexture, nameTexture);
    }

    public void Load()
    {
        OpenPNG(renderTexture, nameTexture);
    }

    void SavePNG(RenderTexture renderTexture, string nameFile)
    {
        RenderTexture.active = renderTexture;

        Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height);
        tex.ReadPixels(new Rect(0, 0, tex.width, tex.height), 0, 0);
        tex.Apply();

        var bytes = tex.EncodeToPNG();
        Destroy(tex);

        File.WriteAllBytes(Application.dataPath + "/Data/" + nameFile + ".png", bytes);

        RenderTexture.active = null;
    }

    void OpenPNG(RenderTexture renderTexture, string nameFile)
    {
        if (File.Exists(Application.dataPath + "/Data/" + nameFile + ".png"))
        {

            byte[] bytes = File.ReadAllBytes(Application.dataPath + "/Data/" + nameFile + ".png");
            Texture2D tex = new Texture2D(renderTexture.width, renderTexture.height);
            tex.LoadImage(bytes);
            tex.Apply();

            Graphics.Blit(tex, renderTexture);


        }
    }
}
