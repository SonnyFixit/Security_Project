using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Color OutlineColor { get; set; }
    [SerializeField]
    public Color outlineColor;

    public virtual void OnMouseDown()
    {
        Debug.Log("Klikni�to na: " + name);
    }

    public void OnMouseEnter()
    {
        Debug.Log("Mysz wesz�a na: " + name);
    }

    public void OnMouseExit()
    {
        Debug.Log("Mysz wysz�a z: " + name);
    }
}
