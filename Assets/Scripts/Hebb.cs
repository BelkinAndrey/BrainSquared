using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hebb : Act
{
    public ComputeShader shader;
    public RenderTexture Synapse;
    public RenderTexture cortex;
    public Slider sliderPlastic;
    public Slider sliderNumber;
    private int kiMain = 0;
    private RenderTexture outTexture;
    private int number = 0;


    void Start()
    {
        outTexture = new RenderTexture(Synapse.width, Synapse.height, 0, RenderTextureFormat.ARGB32);
        outTexture.enableRandomWrite = true;
        outTexture.Create();
        outTexture.filterMode = FilterMode.Point;
    }

    private void Init()
    {
        shader.SetTexture(kiMain, "SynapseRW", outTexture);
        shader.SetTexture(kiMain, "Synapse", Synapse);
        shader.SetTexture(kiMain, "cortex", cortex);
        
        shader.SetFloat("plastic", sliderPlastic.value);
    }

    private void SetNumber(int n)
    {
        number = n;
    }

    private void InitNumber()
    {
        shader.SetInt("number", number);
    }

    private void Calculate()
    {
        shader.Dispatch(kiMain, 1, 10, 1);
        Graphics.Blit(outTexture, Synapse);
    }


    public override void Run(int i)
    {
        Init();
        SetNumber(i);
        InitNumber();
        Calculate();
    } 

    public override void StartAct()
    {
        //
    }

    public void RunGebb()
    {
        Init();
        SetNumber((int)sliderNumber.value);
        InitNumber();
        Calculate();
    }
    
}
