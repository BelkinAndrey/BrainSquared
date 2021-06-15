using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SumHebb : Act
{
    public ComputeShader shader;
    public RenderTexture synapse;
    public RenderTexture cortex;
    public RenderTexture outTex;

    public Slider Sensitivity;

    private int SIZE = 32;
    private int kiMain = 0;
    private RenderTexture outTexture;

    void Start()
    {
        outTexture = new RenderTexture(outTex.width, outTex.height, 0, RenderTextureFormat.ARGB32);
        outTexture.enableRandomWrite = true;
        outTexture.Create();
        outTexture.filterMode = FilterMode.Point;
    }

    private void Init()
    {
        shader.SetTexture(kiMain, "outTexture", outTexture);
        shader.SetTexture(kiMain, "synapse", synapse);
        shader.SetTexture(kiMain, "cortex", cortex);
        shader.SetInt("SIZE", SIZE);
        shader.SetFloat("Sensitivity", Sensitivity.value);
    }

    private void Calculate()
    {
        shader.Dispatch(kiMain, 1, 1, 1);
        Graphics.Blit(outTexture, outTex);
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
