using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MoveTargetBlock : MonoBehaviour
{
    public LayerMask hitLayer;
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Vector3 mousePos = Input.mousePosition;
            Ray rayCastPoint = Camera.main.ScreenPointToRay(mousePos);
            RaycastHit hit;
            if (Physics.Raycast(rayCastPoint, out hit, Mathf.Infinity, hitLayer))
            {
                this.transform.position = hit.point;
            }
        }
    }
}
