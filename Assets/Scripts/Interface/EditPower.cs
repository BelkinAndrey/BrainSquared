using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditPower : MonoBehaviour
{
    public Material material;
    public Slider Power;

    void Start()
    {
        material.SetFloat("_Power", Power.value);
    }

    public void ChangePower(float p)
    {
        material.SetFloat("_Power", p);
    }
}
