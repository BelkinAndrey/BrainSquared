using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingMatButton : MonoBehaviour
{
    public GameObject plane;

    public Texture texture;

    public Material mat;



    public void Push()
    {
        plane.GetComponent<Renderer>().material = mat;
        plane.GetComponent<Renderer>().material.mainTexture = texture;

    }
}
