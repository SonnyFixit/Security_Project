using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{

    public GameObject deactivateSection;
    public GameObject activateSection;


    public GameObject fadePanel;

    
    public GameObject activateCamera1;
    public GameObject deactivateCamera1;
    

    public AudioSource doorSound;

    void Start()
    {
        fadePanel.SetActive(false);
    }

    public virtual void OnMouseDown()
    {

        doorSound.Play();

        

        SwitchCameras();

        StartCoroutine("RoomChange");

    }


  


    public void DeactivateSection()
    {
        deactivateSection.SetActive(false);
    }

    void SwitchCameras()
    {
        activateCamera1.SetActive(true);
        

        deactivateCamera1.SetActive(false);
        
    }

    IEnumerator RoomChange()
    {
        fadePanel.SetActive(true);


        yield return new WaitForSeconds(1.5f);

        fadePanel.SetActive(false);

    }


}

