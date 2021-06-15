using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HebbLearning : MonoBehaviour
{
    public NumberScript figures;
    public Image progressBar;
    public RenderTexture input;
    public RenderTexture retina;
    public Slider Semples;
    public Act[] acts;
    private int semples = 1;

    public void Run()
    {
        semples = (int)Semples.value;
        StopCoroutine(Training());
        StartCoroutine(Training());
    }

    IEnumerator Training()
    {
        float progress = 0f;
        for (int i = 0; i < semples; i++){
            for (int n = 0; n < 10; n++)
            {
                Graphics.Blit(figures.texs[n][i], retina);
                Graphics.Blit(retina, input);
                foreach(Act val in acts) val.Run(n);
                yield return new WaitForEndOfFrame();
            }
            
            progress = (float)i/figures.texs[0].Length;
            progressBar.fillAmount = progress;

            Resources.UnloadUnusedAssets();
        }

        progressBar.fillAmount = 0f;
        
    }
}
