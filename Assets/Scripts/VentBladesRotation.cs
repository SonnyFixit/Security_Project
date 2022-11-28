using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentBladesRotation : MonoBehaviour
{

    [SerializeField]
    private float rotateSpeed;
    
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 100f * rotateSpeed * Time.deltaTime);
    }

}
