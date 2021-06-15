using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplaySettings : MonoBehaviour
{
    public GameObject plane;
    public Material mat1;
    public Material mat2;
    public RenderTexture tex1;
    public RenderTexture tex2;
    public Toggle toggle1;
    public Toggle toggle2;

    public void Run()
    {
        if (toggle1.isOn) plane.GetComponent<Renderer>().material = mat1;
        else plane.GetComponent<Renderer>().material = mat2;

        if (toggle2.isOn) plane.GetComponent<Renderer>().material.mainTexture = tex2;
        else plane.GetComponent<Renderer>().material.mainTexture = tex1;
    }
}
