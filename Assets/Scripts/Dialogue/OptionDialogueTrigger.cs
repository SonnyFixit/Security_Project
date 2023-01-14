using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionDialogueTrigger : MonoBehaviour
{
    public OptionDialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartOptionsDialogue(dialogue);
    }
}
