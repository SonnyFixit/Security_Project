using System;
using UnityEngine;

public class DialogueObject : InteractableObject
{
    public override void OnMouseDown()
    {
        Debug.Log("Kliknięto na interakcje: " + name);
    }
}
