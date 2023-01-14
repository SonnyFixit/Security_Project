using System;
using UnityEngine;

public class PickableObject : InteractableObject
{
    public override void OnMouseDown()
    {
        Debug.Log("Kliknięto na przedmiot: " + name);
    }
}
