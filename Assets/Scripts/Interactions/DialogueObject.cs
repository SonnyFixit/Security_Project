using System;
using UnityEngine;

[RequireComponent(typeof(DialogueTrigger))]
[RequireComponent(typeof(Collider2D))]
public class DialogueObject : InteractableObject
{
    public override void OnMouseDown()
    {
        GetComponent<DialogueTrigger>().TriggerDialogue();
    }
}
