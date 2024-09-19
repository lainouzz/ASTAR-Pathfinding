using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private Grid grid;
    public Transform startPosition;
    public Transform TargetPosition;
    
    public bool pathRecalculated = true;
    
    public void Awake()
    {
        grid = GetComponent<Grid>();
    }

    private void Update()
    {
        if (pathRecalculated)
        {
            FindPath(startPosition.position, TargetPosition.position);
            pathRecalculated = false;
        }
    }

    public void FindPath(Vector3 a_startPosition, Vector3 a_targetPosition)
    {
        Node StartNode = grid.NodeFromWorldPosition(a_startPosition);
        Node TargetNode = grid.NodeFromWorldPosition(a_targetPosition);

        List<Node> open = new List<Node>();
        HashSet<Node> closed = new HashSet<Node>();
        
        open.Add(StartNode);

        while (open.Count > 0)
        {
            Node currentNode = open[0];
            for (int i = 1; i < open.Count; i++)
            {
                if (open[i].FCost < currentNode.FCost ||
                    open[i].FCost == currentNode.FCost && open[i].hCost < currentNode.hCost)
                {
                    currentNode = open[i];
                }
            }

            open.Remove(currentNode);
            closed.Add(currentNode);

            if (currentNode == TargetNode)
            {
                return;
            }

            foreach (Node NeighborNode in grid.GetNeighboringNodes(currentNode))
            {
                if (!NeighborNode.isWall || closed.Contains(NeighborNode))
                {
                    continue;
                }

                int MoveCost = currentNode.gCost + GetManhattenDistance(currentNode, NeighborNode);
                
                if (MoveCost < currentNode.gCost || !open.Contains(NeighborNode))
                {
                    NeighborNode.gCost = MoveCost;
                    NeighborNode.hCost = GetManhattenDistance(NeighborNode, TargetNode);
                    NeighborNode.parent = currentNode;

                    if (!open.Contains(NeighborNode))
                    {
                        open.Add(NeighborNode);
                    }
                }
            }
        }

        grid.FinalPath = GetFinalPath(StartNode, TargetNode);
    }

    public List<Node> GetFinalPath(Node a_startNode, Node a_targetNode)
    {
        List<Node> finalPath = new List<Node>();

        Node currentNode = a_targetNode;

        while (currentNode != a_startNode)
        {
            finalPath.Add(currentNode);
            currentNode = currentNode.parent;
        }
        
        finalPath.Reverse();
        return grid.FinalPath = finalPath;
    }
    
    private int GetManhattenDistance(Node a_nodeA, Node a_nodeB)
    {
        int x = Mathf.Abs(a_nodeA.gridX - a_nodeB.gridX);
        int y = Mathf.Abs(a_nodeA.gridY - a_nodeB.gridY);
        return x + y;
    }
}
