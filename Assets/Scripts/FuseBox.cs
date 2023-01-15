using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour
{


    public GameObject electricityEffect;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip switchClip;

    private int hitsToDestroy = 0;

    public static bool fuseBoxDestroyed = false;


    public virtual void OnMouseDown()
    {

        if (hitsToDestroy <=3)
        {
            audioSource.Play();
        }

        if (hitsToDestroy == 4)
        {
            audioSource.clip = switchClip;
            audioSource.Play();
            fuseBoxDestroyed = true;
            Destroy(electricityEffect);
        }

        hitsToDestroy++;



      
    }

    
        
    
  


}
