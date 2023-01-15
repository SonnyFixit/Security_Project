using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPanels : MonoBehaviour
{
    [SerializeField]
    private GameObject doorLight;

    [SerializeField]
    private AudioSource audioSource;
   
    public static bool firstButton = false;



    public virtual void OnMouseDown()
    {

        if (firstButton == false)
        {
            audioSource.Play();
        }


        doorLight.SetActive(true);
        firstButton = true;

    }

   

}
