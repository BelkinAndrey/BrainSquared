using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Summation : Act
{
    public ComputeShader shader;
    public RenderTexture inputTex;
    public RenderTexture synapse;
    public RenderTexture cortex;

    private int kiMain;
    private RenderTexture outTexture;


    private void Init()
    {
        outTexture = new RenderTexture(cortex.width, cortex.height, 0, RenderTextureFormat.ARGB32);
        outTexture.enableRandomWrite = true;
        outTexture.Create();
        outTexture.filterMode = FilterMode.Point;

        kiMain = shader.FindKernel("CSMain");
        shader.SetTexture(kiMain, "outTexture", outTexture);
        shader.SetTexture(kiMain, "synapse", synapse);
        shader.SetTexture(kiMain, "inputTex", inputTex);
        shader.SetTexture(kiMain, "cortex", cortex);
        shader.SetInt("SIZE", 28);
    }

    private void Calculate()
    {
        shader.Dispatch(kiMain, 1, 1, 1);
        Graphics.Blit(outTexture, cortex);
    } 

    public override void Run(int i)
    {
        Init();
        Calculate();
    } 

    public override void StartAct()
    {
        //
    }
}
