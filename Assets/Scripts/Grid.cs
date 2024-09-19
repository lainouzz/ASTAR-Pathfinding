using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform startPosition;
    public LayerMask wallLayer;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    public float distance;

    private Rect dirtyRect;
    private Node[,] grid;
    public List<Node> FinalPath;

    private float nodeDiameter;
    private int gridSizeX, gridSizeY;

    private void Start()
    {
        nodeDiameter = nodeRadius * 2;

        gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
        gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);
        CreateGrid();
    }

    public void UpdateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        CreateGrid();
    }
    
    private void CreateGrid()
    {
        grid = new Node[gridSizeX, gridSizeY];
        Vector3 bottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 -
                             Vector3.forward * gridWorldSize.y / 2;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = bottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) +
                                     Vector3.forward * (y * nodeDiameter + nodeRadius);
                bool Wall = !Physics.CheckSphere(worldPoint, nodeRadius, wallLayer);

                grid[x, y] = new Node(Wall, worldPoint, x, y);
            }
        }
    }

    public Node NodeFromWorldPosition(Vector3 a_WorldPosition)
    {
        float xPoint = ((a_WorldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x);
        float yPoint = ((a_WorldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y);

        xPoint = Mathf.Clamp01(xPoint);
        yPoint = Mathf.Clamp01(yPoint);

        int x = Mathf.RoundToInt((gridSizeX - 1) * xPoint);
        int y = Mathf.RoundToInt((gridSizeY - 1) * yPoint);

        return grid[x, y];
    }
    
    public List<Node> GetNeighboringNodes(Node a_Node)
    {
        List<Node> neighboringNodes = new List<Node>();
        int xCheck;
        int yCheck;

        //Right Side
        xCheck = a_Node.gridX + 1;
        yCheck = a_Node.gridY;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }
        
        //Left Side
        xCheck = a_Node.gridX - 1;
        yCheck = a_Node.gridY;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }
        
        //Top Side
        xCheck = a_Node.gridX;
        yCheck = a_Node.gridY + 1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }
        
        //Bottom side
        xCheck = a_Node.gridX;
        yCheck = a_Node.gridY - 1;
        if (xCheck >= 0 && xCheck < gridSizeX)
        {
            if (yCheck >= 0 && yCheck < gridSizeY)
            {
                neighboringNodes.Add(grid[xCheck, yCheck]);
            }
        }

        return neighboringNodes;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1 , gridWorldSize.y));
        if (grid != null)
        {
            foreach (Node node in grid)
            {
                if (node.isWall)
                {
                    Gizmos.color = Color.white;
                }
                else
                {
                    Gizmos.color = Color.yellow;
                }

                if (FinalPath != null)
                {
                    if (FinalPath.Contains(node))
                    {
                        Gizmos.color = Color.red;
                    }
                   /* Gizmos.color = Color.red;
                    for (int i = 0; i < FinalPath.Count - 1; i++)
                    {
                        Gizmos.DrawLine(FinalPath[i].position, FinalPath[i + 1].position);
                    }*/
                    
                }
                
                Gizmos.DrawCube(node.position, Vector3.one * (nodeDiameter - distance)); 
            }
        }
    }
}
