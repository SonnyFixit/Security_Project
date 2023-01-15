using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Color outlineColor;

    [SerializeField]
    private Material outlineMaterial;

    [SerializeField]
    private Material normalMaterial;

    private Material currentMaterial;


    public virtual void OnMouseDown()
    {
        Debug.Log("Klikniêto na: " + name);
        
    }

    public void OnMouseEnter()
    {
        Debug.Log("Mysz wesz³a na: " + name);
        currentMaterial = GetComponent<Renderer>().material = outlineMaterial;

    }

    public void OnMouseExit()
    {
        Debug.Log("Mysz wysz³a z: " + name);
        currentMaterial = GetComponent<Renderer>().material = normalMaterial;
    }
}
