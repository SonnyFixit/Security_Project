using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPanels : MonoBehaviour
{

    public GameObject doorLight;
    public GameObject icon;
    public static bool firstButton = false;

    void OnTriggerStay2D(Collider2D col)
    {


        if (col.gameObject.tag == "Player")
        {


            icon.SetActive(true);




            if (Input.GetKeyDown(KeyCode.F))
            {

                doorLight.SetActive(true);
                firstButton = true;

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
