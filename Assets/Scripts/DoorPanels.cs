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
    private QuestionTrigger questionTrigger;

    private void Start()
    {
        questionTrigger = GetComponent<QuestionTrigger>();
    }


    public virtual void OnMouseDown()
    {
        if (firstButton == false)
        {
            audioSource.Play();
        }
    }
    private void Update()
    {
        if (firstButton)
            return;

        if (questionTrigger.question.answererdCorrectly)
        {
            firstButton = true;
            doorLight.SetActive(true);
            GetComponent<QuestionObject>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
