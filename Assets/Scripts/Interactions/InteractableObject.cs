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
    }

    public void OnMouseEnter()
    {
        currentMaterial = GetComponent<Renderer>().material = outlineMaterial;

    }

    public void OnMouseExit()
    {
        currentMaterial = GetComponent<Renderer>().material = normalMaterial;
    }
}
