using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBlackBoard: MonoBehaviour
{
 
    public Transform target;
    public float step = 20f;
    

    public static bool blackBoardMoved = false;


   
    public virtual void OnMouseDown()
    {

        if (blackBoardMoved == false)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, step * Time.deltaTime);
        }

        if (transform.position.x == target.transform.position.x)
        {
            blackBoardMoved = true;
        }

    }


}
