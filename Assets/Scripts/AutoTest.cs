using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class AutoTest : MonoBehaviour
{
    public RectTransform TestPanel;
    public Text ButtonText;
    public RectTransform ButtonHide;
    public Text Total;
    public Text sight;
    public Text error;
    public Text quality;
    public Image progressBar;
    public RenderTexture input;
    public RenderTexture retina;
    public Act[] acts;

    

    private bool isTest = false;
    private Texture2D texture;
    private string[][] pach = new string[10][];
    private int TotalEx = 0;
    private int Sight = 0;
    private int Error = 0;
    private float Quality = 0f;

    private int GetEx()
    {
        int total = 0;
        for (int i = 0; i < 10; i++)
        {
            total += pach[i].Length;
        }
        return total;
    }



    private void LoadNumbersPach()
    {
        pach[0] = Directory.GetFiles(Application.dataPath + "/Data/mnist_png/testing/0/", "*.png");
        pach[1] = Directory.GetFiles(Application.dataPath + "/Data/mnist_png/testing/1/", "*.png");
        pach[2] = Directory.GetFiles(Application.dataPath + "/Data/mnist_png/testing/2/", "*.png");
        pach[3] = Directory.GetFiles(Application.dataPath + "/Data/mnist_png/testing/3/", "*.png");
        pach[4] = Directory.GetFiles(Application.dataPath + "/Data/mnist_png/testing/4/", "*.png");
        pach[5] = Directory.GetFiles(Application.dataPath + "/Data/mnist_png/testing/5/", "*.png");
        pach[6] = Directory.GetFiles(Application.dataPath + "/Data/mnist_png/testing/6/", "*.png");
        pach[7] = Directory.GetFiles(Application.dataPath + "/Data/mnist_png/testing/7/", "*.png");
        pach[8] = Directory.GetFiles(Application.dataPath + "/Data/mnist_png/testing/8/", "*.png");
        pach[9] = Directory.GetFiles(Application.dataPath + "/Data/mnist_png/testing/9/", "*.png");
    }

    private Texture2D LoadTexture(string p)
    {
        byte[] bytes;
        bytes = File.ReadAllBytes(p);
        Texture2D tex = new Texture2D(28, 28);
        tex.filterMode = FilterMode.Point;
        tex.LoadImage(bytes);
        tex.Apply();
        Texture2D flipped = new Texture2D(28, 28);
        Color[] pixels = tex.GetPixels();
        System.Array.Reverse(pixels, 0, pixels.Length);
        flipped.SetPixels(pixels);
        flipped.Apply();
        return flipped;

    }

    IEnumerator Test()
    {
        float progress = 0f;
        for (int n = 0; n < 10; n++){
            for (int i = 0; i < pach[n].Length; i++)
            {
                texture = LoadTexture(pach[n][i]);
                Graphics.Blit(texture, input);
                Graphics.Blit(texture, retina);

                for (int j = 0; j < acts.Length; j++) 
                { 
                    acts[j].Run(0);
                    yield return new  WaitForSeconds(0.0001f);
                }

                if (n != (acts[acts.Length - 1] as OutMax).GetTest()) Error++; 
                
                Sight++;
                sight.text = "" + Sight;
                error.text = "" + Error;
                Quality = (float)(Sight - Error)/Sight; 
                Quality *= 100;
                quality.text = Quality.ToString("0.00") + " %";
                progress = (float)Sight/TotalEx;
                progressBar.fillAmount = progress;

                Resources.UnloadUnusedAssets();
            }
        }

        progressBar.fillAmount = 0f;
        ButtonText.text = "Test";
        ButtonHide.gameObject.SetActive(true);
        isTest = false;
        
    }

    public void StartTest()
    {
        if (isTest) 
        {
            StopAllCoroutines();
            ButtonText.text = "Test";
            progressBar.fillAmount = 0f;
            ButtonHide.gameObject.SetActive(true);
            isTest = false;
     
        } else {
            LoadNumbersPach();
            TestPanel.gameObject.SetActive(true);
            ButtonHide.gameObject.SetActive(false);
            TotalEx = GetEx();
            Sight = 0;
            Total.text = "" + TotalEx;
            error.text = "0";
            Error = 0;
            Quality = 0f;
            quality.text = "- %";
            StopAllCoroutines();
            StartCoroutine(Test());
            isTest = true;
            ButtonText.text = "Stop";
        }
    }

    public void HidePanelTest()
    {
        TestPanel.gameObject.SetActive(false);
    }

}
