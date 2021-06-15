using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderText : MonoBehaviour {

    void Start ()
    {
        ChangeSlider(GetComponentInParent<Slider>().value);
    }

    public void ChangeSlider(Single value) 
    {
        gameObject.GetComponent<Text>().text = value.ToString();
    }
}
