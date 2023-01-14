using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour
{

    public GameObject wall;
    public GameObject electricityEffect;
    public GameObject electricExplosion;
    public GameObject icon;



    void OnTriggerStay2D(Collider2D col)
    {


    if (col.gameObject.tag == "Player")
    {


        icon.SetActive(true);




        if (Input.GetKeyDown(KeyCode.F))
        {
 
        Instantiate(electricExplosion, transform.position, transform.rotation);
        Destroy(wall, 3f);
        Destroy(electricityEffect, 3f);
        }
    
        
    }
    }



    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {


        icon.SetActive(false);
        }
    }

}
