using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eduction : Act
{
    public RenderTexture inputTex;
    public RenderTexture outTex;
    public Material mat;
    private RenderTexture tempTex;

    void Start()
    {
        tempTex = new RenderTexture(inputTex.width, inputTex.height, inputTex.depth);
    }

    public override void StartAct()
    {
        //
    }

    public override void Run(int i)
    {
        Graphics.Blit(inputTex, tempTex, mat);
        Graphics.Blit(tempTex, outTex);
    } 
}
