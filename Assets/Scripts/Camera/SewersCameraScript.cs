using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SewersCameraScript : MonoBehaviour
{

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private float timeOffset;

    [SerializeField]
    private Vector2 positionOffset;

    private Vector3 velocity;

    [SerializeField]
    private float zoom;

    [SerializeField]
    private float minZoomSize;

    [SerializeField]
    private float maxZoomSize;

    private Vector3 dragOrigin;


    public float leftBound;

    public float rightBound;

    public float downBound;

    public float upperBound;

    float zAxisBound = -10f;

    public float manualSpeed = 5.0f;



    // Update is called once per frame
    void Update()
    {
        ZoomCamera();

        if (FuseBox.fuseBoxDestroyed == false)
        {
            rightBound = 103.5f;
        }

        else if (FuseBox.fuseBoxDestroyed == true)
        {
            rightBound = 192.4f;
        }
    }

    void LateUpdate()
    {
        MoveWithMouse();
    }


    private void MoveWithMouse()
    {


        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = camera.ScreenToWorldPoint(Input.mousePosition);


        }

        if (Input.GetMouseButton(1))
        {

            Vector3 difference = dragOrigin - camera.ScreenToWorldPoint(Input.mousePosition);


            camera.transform.position += difference;


            camera.transform.position = new Vector3(
               Mathf.Clamp(transform.position.x, leftBound, rightBound),
               Mathf.Clamp(transform.position.y, downBound, upperBound),
               transform.position.z);


        }


    }



    private void ZoomCamera()
    {

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {


            float newSize = camera.orthographicSize - zoom;

            camera.orthographicSize = Mathf.Clamp(newSize, minZoomSize, maxZoomSize);
        }


        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {


            float newSize = camera.orthographicSize + zoom;

            camera.orthographicSize = Mathf.Clamp(newSize, minZoomSize, maxZoomSize);
        }

    }


}
