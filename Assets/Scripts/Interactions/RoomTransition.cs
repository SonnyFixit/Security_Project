using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{

    public GameObject deactivateSection;
    public GameObject activateSection;

    [SerializeField]
    private AudioClip deniedAccessClip;
    [SerializeField]
    private AudioClip grantedAccessClip;

    [SerializeField]
    private AudioSource audioSource;

    public GameObject fadePanel;


    public GameObject activateCamera1;
    public GameObject deactivateCamera1;

    public virtual void OnMouseDown()
    {
        if (DoorPanels.firstButton == true && DoorPanels2.secondButton == true)
        {
            audioSource.clip = grantedAccessClip;
            audioSource.Play();

            SwitchCameras();
            StartCoroutine("RoomChange");
            return;
        }

        GetComponent<DialogueTrigger>().TriggerDialogue();
        audioSource.clip = deniedAccessClip;
        audioSource.Play();
        return;
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

        fadePanel.SetActive(false);


        fadePanel.SetActive(true);


        yield return new WaitForSeconds(1.5f);

        fadePanel.SetActive(false);
        DeactivateSection();

    }


}

