using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGates : MonoBehaviour
{


    public virtual void OnMouseDown()
    {
        if (MainGatesButtonOne.firstGateUnlock == true)
        {
            Application.Quit();
        }
    }
   
       
    
}
