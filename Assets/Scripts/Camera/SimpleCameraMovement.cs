using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraMovement : MonoBehaviour
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

    [SerializeField]
    float leftBound;
    [SerializeField]
    float rightBound;
    [SerializeField]
    float downBound;
    [SerializeField]
    float upperBound;
    [SerializeField]
    float zAxisBound = -10f;

    public float manualSpeed = 5.0f;



    // Update is called once per frame
    void Update()
    {
        ZoomCamera();
    }

    void LateUpdate()
    {
        MoveWithMouse();
    }


    private void MoveWithMouse()
    {


        if (Input.GetMouseButtonDown(2))
        {
            dragOrigin = camera.ScreenToWorldPoint(Input.mousePosition);
           

        }

        if (Input.GetMouseButton(2))
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
