using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainLevelTransitions : MonoBehaviour
{
    public GameObject deactivateSection;
    public GameObject activateSection;

   

    [SerializeField]
    private AudioSource audioSource;

    public GameObject fadePanel;


    public GameObject activateCamera1;
    public GameObject deactivateCamera1;




    public virtual void OnMouseDown()
    {



       
            audioSource.Play();



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

        fadePanel.SetActive(false);


        fadePanel.SetActive(true);


        yield return new WaitForSeconds(1.5f);

        fadePanel.SetActive(false);
        DeactivateSection();

    }
}
