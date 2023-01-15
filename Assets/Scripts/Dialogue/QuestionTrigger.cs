using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class QuestionTrigger : MonoBehaviour
{
    public Question question;
    public void TriggerQuestion()
    {
        FindObjectOfType<DialogueManager>().StartQuestion(question);
    }
}
