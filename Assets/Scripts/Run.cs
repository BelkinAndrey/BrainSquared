using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonoBehaviour
{
    public Act[] acts;

    public void RunNet()
    {
        for (int i = 0; i < acts.Length; i++)
        {
            acts[i].Run(0);
        }

        Resources.UnloadUnusedAssets();
    }
}
