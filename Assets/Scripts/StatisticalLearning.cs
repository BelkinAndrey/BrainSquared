using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class StatisticalLearning : MonoBehaviour
{
    public RenderTexture Input;
    public RenderTexture Retina;

    public Slider sliderTime;
    public Text textTotal;
    public Slider sliderLimit;
    public Toggle togglePlay;
    public Toggle toggleT;
    public Act[] acts;
    public Act[] actsT;
    private int IterDefolt;

    bool RunBool = false;
    float timeStep = 0.0001f;
    int total = 0;
    int limit = 1000;

    private string[][] pach = new string[10][];


    private void LoadNumbersPach()
    {
        pach[0] = Directory.GetFiles(Application.dataPath + "/Data/mnist_png/training/0/", "*.png");
        pach[1] = Directory.GetFiles(Application.dataPath + "/Data/mnist_png/training/1/", "*.png");
        pach[2] = Directory.GetFiles(Application.dataPath + "/Data/mnist_png/training/2/", "*.png");
        pach[3] = Directory.GetFiles(Application.dataPath + "/Data/mnist_png/training/3/", "*.png");
        pach[4] = Directory.GetFiles(Application.dataPath + "/Data/mnist_png/training/4/", "*.png");
        pach[5] = Directory.GetFiles(Application.dataPath + "/Data/mnist_png/training/5/", "*.png");
        pach[6] = Directory.GetFiles(Application.dataPath + "/Data/mnist_png/training/6/", "*.png");
        pach[7] = Directory.GetFiles(Application.dataPath + "/Data/mnist_png/training/7/", "*.png");
        pach[8] = Directory.GetFiles(Application.dataPath + "/Data/mnist_png/training/8/", "*.png");
        pach[9] = Directory.GetFiles(Application.dataPath + "/Data/mnist_png/training/9/", "*.png");
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

    public void Run()
    {
        int num = Random.Range(0, 10);
        int num2 = Random.Range(0, pach[num].Length);
        Texture2D texture = LoadTexture(pach[num][num2]);
        Graphics.Blit(texture, Input);
        Graphics.Blit(texture, Retina);

        if (toggleT.isOn)
        {
            for (int i = 0; i < actsT.Length; i++)
            {
                actsT[i].Run(num);
            }

        } else {
            for (int i = 0; i < acts.Length; i++)
            {
                acts[i].Run(IterDefolt);
            }
        }


        total++;
        if (total >= limit) 
        {
            togglePlay.isOn = false;
            ChangPlayButton(false);
        }

        textTotal.text = total.ToString();

        Resources.UnloadUnusedAssets();
    }

    IEnumerator update()
    {
        while (true)
        {
            if (RunBool) Run();
            yield return new WaitForSeconds(timeStep);
        }
    }


    void Start()
    {
        timeStep = sliderTime.value;
        limit = (int)sliderLimit.value;
        LoadNumbersPach();
        StartCoroutine(update());
    }

    public void ChangPlayButton(bool value)
    {
        RunBool = value;
        if (value) 
        { 
            total = 0;
            for (int i = 0; i < acts.Length; i++) acts[i].StartAct();
            for (int i = 0; i < actsT.Length; i++) actsT[i].StartAct();
        }
        
    }

    public void ChangLimit(float value)
    {
        limit = (int)value;
    }

    public void ChangeTimeStep(float value)
    {
        timeStep = value;
    }
}
