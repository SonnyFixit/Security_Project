using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGates : MonoBehaviour
{
    public bool firstLightUnlock;
    public bool secondLightUnlock;

    public virtual void OnMouseDown()
    {
        if (firstLightUnlock && secondLightUnlock)
        {
            Debug.Log("KONIEC");
            Application.Quit();
        }
    }
}
