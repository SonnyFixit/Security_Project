using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class InteractableObject : MonoBehaviour
{
    [SerializeField]
    private Material outlineMaterial;

    private Material normalMaterial;

    private Material currentMaterial;
    private Renderer materialRenderer;

    private void Start()
    {
        materialRenderer = GetComponent<Renderer>();
        if (materialRenderer != null)
            normalMaterial = materialRenderer.material;
    }


    public virtual void OnMouseDown()
    {
    }

    public void OnMouseEnter()
    {
        if (outlineMaterial != null)
            currentMaterial = GetComponent<Renderer>().material = outlineMaterial;
    }

    public void OnMouseExit()
    {
        if (normalMaterial != null)
            currentMaterial = GetComponent<Renderer>().material = normalMaterial;
    }
}
