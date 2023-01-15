using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPanels2 : MonoBehaviour
{
    [SerializeField]
    private GameObject doorLight;

    [SerializeField]
    private AudioSource audioSource;

    public static bool secondButton = false;



    public virtual void OnMouseDown()
    {

        if (secondButton == false)
        {
            audioSource.Play();
        }


        doorLight.SetActive(true);
        secondButton = true;

    }



}
