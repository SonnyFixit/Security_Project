using System;
using UnityEngine;

public class QuestionObject : InteractableObject
{
    public override void OnMouseDown()
    {
        GetComponent<QuestionTrigger>().TriggerQuestion();
    }
}