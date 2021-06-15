using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Statistical : Act
{
    public ComputeShader shader;
    public RenderTexture inputTex;
    public RenderTexture synapse;
    public RenderTexture cortex;

    public Slider Plastic;
    public Slider Learnability;
    public Slider UseUp;
    public Slider UseDown;
    public Slider RadiusStart;
    public Slider TempR;
    public Slider LTemp;

    private int kiMain = 0;
    private int kiMinMax = 1;
    private int kiUse = 2;
    private int kiUseUp = 3;
    private RenderTexture outTexture;
    private RenderTexture outCortex;
    int[] _MinMax = {1, 0, 0};
    int[] _XY = {-1, -1};
    ComputeBuffer MinMax;
    ComputeBuffer XY;

    public Text TMin;
    public Text TMax;
    public Text TRadius;

    private int R;
    private int T = 0;

    //TempDown

    private int step;
    public Slider SliderTemp;

    public Slider rateStart;
    public Slider upStart;
    public Slider upDrop;



    void Start()
    {
        outTexture = new RenderTexture(synapse.width, synapse.height, 0, RenderTextureFormat.ARGB32);
        outTexture.enableRandomWrite = true;
        outTexture.Create();
        outTexture.filterMode = FilterMode.Point;

        outCortex = new RenderTexture(cortex.width, cortex.height, 0, RenderTextureFormat.ARGB32);
        outCortex.enableRandomWrite = true;
        outCortex.Create();
        outCortex.filterMode = FilterMode.Point;

        MinMax = new ComputeBuffer(_MinMax.Length, sizeof(int));
        MinMax.SetData(_MinMax);

        XY = new ComputeBuffer(_XY.Length, sizeof(int));
        XY.SetData(_XY);

        R = (int)RadiusStart.value;

        step = 0;   
    }


    private void Init()
    {
        shader.SetTexture(kiMain, "outTexture", outTexture);
        shader.SetTexture(kiMain, "synapse", synapse);
        shader.SetTexture(kiMain, "inputTex", inputTex);


        shader.SetInt("SIZE", 28);
        shader.SetInt("R", R);
        shader.SetFloat("Plastic", Plastic.value);
        shader.SetFloat("Learnability", Learnability.value);

        shader.SetBuffer(kiMain, "XY", XY);
    }

    private void InitMinMax()
    {
        _MinMax[0] = 256; 
        _MinMax[1] = 0;
        _MinMax[2] = 0;
        MinMax.SetData(_MinMax);
        shader.SetTexture(kiMinMax, "cortex", cortex);
        shader.SetBuffer(kiMinMax, "MinMax", MinMax);
    }

    private void InitUse()
    {
        shader.SetFloat("_Min", (float)_MinMax[0]/255);
        shader.SetFloat("_Max", (float)_MinMax[1]/255);
        shader.SetInt("_Max2", _MinMax[2]);
        shader.SetTexture(kiUse, "outCortex", outCortex);
        shader.SetTexture(kiUse, "cortex", cortex);

        _XY[0] = -1;
        _XY[1] = -1;
        XY.SetData(_XY);
        shader.SetBuffer(kiUse, "XY", XY);
    }

    private void InitUseUp()
    {
        shader.SetTexture(kiUseUp, "outCortex", outCortex);
        shader.SetTexture(kiUseUp, "cortex", cortex);

        shader.SetFloat("Up", UseUp.value);
        shader.SetFloat("Down", UseDown.value);

        shader.SetBuffer(kiUseUp, "XY", XY);

        shader.SetInt("DownOn", (step == (int)SliderTemp.value)? 1: 0);
    }

    private void Calculate()
    {
        shader.Dispatch(kiMain, 28, 28, 1);
        Graphics.Blit(outTexture, synapse);
    } 

    private void CalculateMinMax()
    {
        shader.Dispatch(kiMinMax, 1, 1, 1);
        MinMax.GetData(_MinMax);

        TMin.text = "Min: " + ((float)_MinMax[0] / 255).ToString("0.000");
        TMax.text = "Max: " + ((float)_MinMax[1] / 255).ToString("0.000");
    } 

    private void CalculateUse()
    {
        shader.Dispatch(kiUse, 1, 1, 1);
        XY.GetData(_XY);
        Graphics.Blit(outCortex, cortex);
    }

    private void CalculateUseUp()
    {
        shader.Dispatch(kiUseUp, 1, 1, 1);
        Graphics.Blit(outCortex, cortex);
    }

    public override void Run(int i)
    {
        step++;


        InitMinMax();
        CalculateMinMax();
        InitUse();
        CalculateUse();
        Init();
        Calculate(); 
        InitUseUp();
        CalculateUseUp();
        
        T++;

        if (T >= TempR.value) 
        {
            T = 0;
            if (R != 0 )R--;
        }

        TRadius.text = "" + R;

        Learnability.value -= LTemp.value;


        if (step > SliderTemp.value) 
        { 
            step = 0;
            UseUp.value -= upDrop.value;
        }
    } 

    void OnDisable()
    {
        XY.Dispose();
        MinMax.Dispose();
    }

    public void SetRadius(int radius)
    {
        R = radius;
    }

    public override void StartAct()
    {
        SetRadius((int)RadiusStart.value);
        T = 0;

        if (LTemp.value > 0) Learnability.value = rateStart.value;
        if (upDrop.value > 0) UseUp.value = upStart.value;

    }
}
