using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStartPosition : MonoBehaviour
{
    public Grid grid;
    public Pathfinding pathfinding;

    private List<Node> path;
    private int currentIndex;
    
    public float moveSpeed;
    public Transform targetPosition;
    
    private bool isMoving;


    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Node startNode = grid.NodeFromWorldPosition(transform.position);
            Node targetNode = grid.NodeFromWorldPosition(targetPosition.position);
            
            pathfinding.FindPath(transform.position, pathfinding.TargetPosition.position);
            pathfinding.GetFinalPath(startNode, targetNode);
            path = grid.FinalPath;
            currentIndex = 0;
            isMoving = true;
        }

        if (isMoving && path != null && currentIndex < path.Count)
        {
            MoveToTarget();
        }
    }

    void MoveToTarget()
    {
        if (currentIndex >= path.Count)
        {
            isMoving = false;
            return;
        }

        Node currentNode = path[currentIndex];
        Vector3 targetPos = currentNode.position;

        Vector3 targetDir = targetPos - transform.position;
        
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, moveSpeed, 0.0f);
        
        transform.rotation = Quaternion.LookRotation(newDir);
        
        Debug.DrawRay(transform.position, newDir, Color.magenta);
        
        if (Vector3.Distance(transform.position, targetPos) < .1f)
        {
            currentIndex++;
        }

    }
}
