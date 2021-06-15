using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTexture : MonoBehaviour
{
    public RenderTexture input;
    public RenderTexture output;

    public void Build()
    {
        Graphics.Blit(input, output);
    }
}
