using System;
using UnityEngine;

public class DialogueObject : InteractableObject
{
    public override void OnMouseDown()
    {
        GetComponent<DialogueTrigger>().TriggerDialogue();
    }
}
