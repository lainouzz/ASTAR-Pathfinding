using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform car;
    public LayerMask HitLayer;

    public float rayDistance;
    
    private Vector3 distance;
    
    private RaycastHit hit;
    // Update is called once per frame
    void FixedUpdate()
    {
        distance = car.transform.position - Camera.main.transform.position;
        
        //CarCheck(hit);
        ObstacleCheck(hit);
    }

    void CarCheck(RaycastHit hit)
    {
        if (Physics.Raycast(distance, Vector3.up, out hit, rayDistance))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.DrawRay(Camera.main.transform.position, distance * 1, Color.yellow);
                Debug.Log("hit something");
            }
        }
    }

    void ObstacleCheck(RaycastHit hit)
    {
        if (Physics.Raycast(distance, Vector3.up, out hit, rayDistance, HitLayer))
        {
            Debug.DrawRay(Camera.main.transform.position, distance * 1, Color.red);
            Debug.Log("Wall hit: " + gameObject.name); 
        }
    }
}
