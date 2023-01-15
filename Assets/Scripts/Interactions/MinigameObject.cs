using System;
using UnityEngine;

public class MinigameObject : InteractableObject
{
    public override void OnMouseDown()
    {
        GetComponent<MinigameTrigger>().TriggerMinigame();
    }
}
