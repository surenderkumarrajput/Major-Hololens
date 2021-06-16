using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeController : MonoBehaviour
{
    public float speed;
    private float currentSpeed;
   
    //Update Function
    void Update()
    {
        transform.Rotate(-Vector3.up * currentSpeed * Time.deltaTime);
    }

    //Function for Starting Rotation
    public void StartRotation()
    {
        currentSpeed = speed;
        Debug.Log("Start");
    }

    //Function for Ending Rotation
    public void EndRotation()
    {
        currentSpeed = 0;
        Debug.Log("End");
    }
}
