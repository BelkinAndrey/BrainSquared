using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutMax : Act
{
    public RenderTexture outTex;
    public Text[] texts;

    private Texture2D output;

    private int test;

    void Start()
    {
        output = new Texture2D(1, 10);
    }

    public override void Run(int i)
    {
        RenderTexture.active = outTex;
        output.ReadPixels(new Rect(0, 0, outTex.width, outTex.height), 0, 0);
        output.Apply();    
        RenderTexture.active = null;

        float a = output.GetPixel(0, 0).r;
        int idexMax = 0;
        for (int k = 0; k < 10; k++)
        {
            texts[k].color = Color.black;

            float b = output.GetPixel(0, k).r;
            if (a < b) 
            {
                a = b;
                idexMax = k;
            }
        }

        int collMax = 0;
        test = -1;
        for (int j = 0; j < 10; j++)
        {
            float c = output.GetPixel(0, j).r;
            if (a == c) collMax++;
        }

        if (collMax == 1) 
        {
            texts[idexMax].color = Color.red;
            test = idexMax;
        }
    } 

    public override void StartAct()
    {
        //
    }

    public int GetTest()
    {
        return test;
    }


}
