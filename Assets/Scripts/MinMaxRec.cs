using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinMaxRec : Act
{
    public ComputeShader shader;
    public RenderTexture cortex;
    private RenderTexture outCortex;
    int[] _MinMax = {1, 0};
    ComputeBuffer MinMax;
    private int kiMinMax = 0;
    private int kiRec = 1;

    public Text TMin;
    public Text TMax;

    void Start()
    {
        outCortex = new RenderTexture(cortex.width, cortex.height, 0, RenderTextureFormat.ARGB32);
        outCortex.enableRandomWrite = true;
        outCortex.Create();
        outCortex.filterMode = FilterMode.Point;

        MinMax = new ComputeBuffer(_MinMax.Length, sizeof(int));
        MinMax.SetData(_MinMax);
    }

    private void InitMinMax()
    {
        _MinMax[0] = 256; 
        _MinMax[1] = 0;
        MinMax.SetData(_MinMax);
        shader.SetTexture(kiMinMax, "cortex", cortex);
        shader.SetBuffer(kiMinMax, "MinMax", MinMax);
    }

    private void CalculateMinMax()
    {
        shader.Dispatch(kiMinMax, 1, 1, 1);
        MinMax.GetData(_MinMax);

        TMin.text = "Min: " + ((float)_MinMax[0] / 255).ToString("0.000");
        TMax.text = "Max: " + ((float)_MinMax[1] / 255).ToString("0.000");
    } 

    private void InitRec()
    {
        shader.SetFloat("_Min", (float)_MinMax[0]/255);
        shader.SetFloat("_Max", (float)_MinMax[1]/255);

        shader.SetTexture(kiRec, "outCortex", outCortex);
        shader.SetTexture(kiRec, "cortex", cortex);
    }

    private void CalculateRec()
    {
        shader.Dispatch(kiRec, 1, 1, 1);
        Graphics.Blit(outCortex, cortex);
    } 


    public override void StartAct()
    {
        //
    }

    public override void Run(int i)
    {
        InitMinMax();
        CalculateMinMax();
        InitRec();
        CalculateRec();
    } 

    void OnDisable()
    {
        MinMax.Dispose();
    }
}
