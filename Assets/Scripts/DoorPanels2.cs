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

    private QuestionTrigger questionTrigger;

    private void Start()
    {
        questionTrigger = GetComponent<QuestionTrigger>();
    }


    public virtual void OnMouseDown()
    {
        if (secondButton == false)
        {
            audioSource.Play();
        }
    }

    private void Update()
    {
        if (secondButton)
            return;

        if (questionTrigger.question.answererdCorrectly)
        {
            secondButton = true;
            doorLight.SetActive(true);
            GetComponent<QuestionObject>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }

}
