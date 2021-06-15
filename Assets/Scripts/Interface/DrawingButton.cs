using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingButton : MonoBehaviour
{
    public GameObject plane;
    public Texture texture;

    public void Push()
    {
        plane.GetComponent<Renderer>().material.mainTexture = texture;
    }
}
