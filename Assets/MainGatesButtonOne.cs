using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGatesButtonOne : MonoBehaviour
{
    public GameObject doorLight;
    public static bool firstGateUnlock;

    public virtual void OnMouseDown()
    {
        doorLight.SetActive(true);
        firstGateUnlock = true;
        FindObjectOfType<MainGates>().firstLightUnlock = true;
    }
}
