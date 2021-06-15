using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject obj;

    public void ChangeToggle(bool b)
    {
        obj.SetActive(b);
    }
}
