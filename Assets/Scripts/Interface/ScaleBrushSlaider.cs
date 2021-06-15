using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleBrushSlaider : MonoBehaviour {

    public void ChangSlaiderScale(float value) 
    {
        gameObject.GetComponent<Image>().rectTransform.sizeDelta = new Vector2(value, value) * 1.6f;
    }
}
