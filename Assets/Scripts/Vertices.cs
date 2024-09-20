using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Vertices : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var mesh = GetComponent<MeshCollider>().bounds.extents; 
        var centerMesh = GetComponent<MeshCollider>().bounds.size;

        Debug.Log("bounds: " + mesh);
        Debug.Log("Size: " + centerMesh);
    }

}
