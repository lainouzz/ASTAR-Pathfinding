using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform car;


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 distance = car.transform.position - Camera.main.transform.position;
        
        RaycastHit hit;
        if (Physics.Raycast(distance, Vector3.up, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(Camera.main.transform.position, distance * hit.distance, Color.yellow);
            Debug.Log("hit something");
        }
    }
}
