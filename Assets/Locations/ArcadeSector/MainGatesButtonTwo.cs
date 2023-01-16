using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGatesButtonTwo : MonoBehaviour
{
    public static GameObject doorLight;
    public static bool firstGateUnlock;

    public GameObject doorLightObject;

    private void Start()
    {
        doorLight = doorLightObject;
    }
}
