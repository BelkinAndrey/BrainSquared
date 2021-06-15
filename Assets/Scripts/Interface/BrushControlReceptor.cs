using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrushControlReceptor : MonoBehaviour {

    RenderTexture renderTexture;

    int x, y;

    int sizeBrush = 1;

    public Slider colorSlider;

    void Start() 
    {
        renderTexture = (RenderTexture)gameObject.GetComponent<MeshRenderer>().material.mainTexture;
        ClearPlane();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out hit))
                return;

            if (hit.collider.name != gameObject.name)
                return;

            Vector2 localPoint = hit.textureCoord;
            x = (int)(localPoint.x * renderTexture.width);
            y = renderTexture.height - (int)(localPoint.y * renderTexture.height) - 1;

            BrushPaint();
        }
    }

    void BrushPaint()
    {

        GL.PushMatrix();
        GL.LoadPixelMatrix(0, renderTexture.width, renderTexture.height, 0);

        RenderTexture.active = renderTexture;

        Texture2D tex = new Texture2D(1, 1);

        tex.SetPixel(0, 0, new Color(colorSlider.value/256, 0, 0, 1));
        
        tex.Apply();

        Graphics.DrawTexture(new Rect(x - sizeBrush / 2, y - sizeBrush / 2, sizeBrush, sizeBrush), tex);

        RenderTexture.active = null;

        GL.PopMatrix();
    }

    public void ClearPlane() 
    {
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, renderTexture.width, renderTexture.height, 0);

        RenderTexture.active = renderTexture;

        Texture2D tex = new Texture2D(1, 1);

        tex.SetPixel(0, 0, new Color(0, 0, 0, 1));

        tex.Apply();

        Graphics.DrawTexture(new Rect(0, 0, renderTexture.width, renderTexture.height), tex);

        RenderTexture.active = null;

        GL.PopMatrix();
    }

    public void ChangSlaiderScale(float value) 
    {
        sizeBrush = (int)value;
    }
}
