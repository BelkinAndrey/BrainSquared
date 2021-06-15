using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class NumberScript : MonoBehaviour
{
    public RenderTexture receptor;
    public BuildTexture buildT;
    private string[][] pach = new string[10][];

    public Texture2D[][] texs;

    public Slider sliderAmount;
    public Slider sliderPage;
    public Slider sliderSemple;

    public Run run;
    
    void Start()
    {
        LoadNumbersPach();
        LoadRandomNumbers();
    }

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

    private void LoadRandomNumbers()
    {
        sliderPage.value = 1;
        sliderPage.maxValue = sliderAmount.value;
        sliderSemple.maxValue = sliderAmount.value * 10;
        int count = 10 * (int)sliderAmount.value; 
        texs = new Texture2D[10][];        

        byte[] bytes;
        
        for (int j = 0; j < 10; j++){

            texs[j] = new Texture2D[count];

            for (int i = 0; i < count; i++){
                bytes = File.ReadAllBytes(pach[j][Random.Range(0, pach[j].Length)]);
                Texture2D tex = new Texture2D(28, 28);
                tex.filterMode = FilterMode.Point;
                tex.LoadImage(bytes);
                tex.Apply();
                texs[j][i] = FlipTexture(tex);
            }
        }


        for (int j = 0; j < 10; j++){
            for (int i = 0; i < 10; i++){
                transform.GetChild(j).GetChild(i).gameObject.GetComponent<Renderer>().material.mainTexture = texs[j][i];
            }
        }
    }

    public void ButtonPage()
    {
        for (int j = 0; j < 10; j++){
            for (int i = 0; i < 10; i++){
                transform.GetChild(j).GetChild(i).gameObject.GetComponent<Renderer>().material.mainTexture = texs[j][i + ((int)sliderPage.value - 1) * 10];
            }
        }
    }

    public void ButtonLoadRandomNumbers()
    {
        LoadRandomNumbers();
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Number")
                    {
                        Texture2D tex = new Texture2D(28, 28);
                        tex = (Texture2D)hit.collider.gameObject.GetComponent<Renderer>().material.mainTexture;
                        Graphics.Blit(tex, receptor);

                        buildT.Build();
                        run.RunNet();

                    }
                }
            }
    }

    Texture2D FlipTexture(Texture2D original)
    {

        Texture2D flipped = new Texture2D(original.width, original.height);
        flipped.filterMode = FilterMode.Point;

        Color[] pixels = original.GetPixels();

        System.Array.Reverse(pixels, 0, pixels.Length);

        flipped.SetPixels(pixels);
        flipped.Apply();

        return flipped;
    }


}
