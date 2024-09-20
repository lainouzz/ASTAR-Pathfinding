using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomWallSpawn : MonoBehaviour
{
    public Grid grid;
    public GameObject[] walls;
    public MeshCollider meshCollider;
    public Transform startPosition;
    
    public float offset;
    public int wallAmount;
    
    public Vector3 minScale = new Vector3(3f, 1.5f, 3f);
    public Vector3 maxScale = new Vector3(3, 2f, 3f);

    private List<Vector3> spawnedWallsPos = new List<Vector3>();
    Vector3 newRandomPos;
    [SerializeField]private float minimumDistance;
    // Start is called before the first frame update
    void Start()
    {
        SpawnAtRandomPos();
    }

    public void SpawnAtRandomPos()
    {
        for (int i = 0; i < wallAmount; i++)
        {
            
            bool validPosition = false;

            while (!validPosition)
            {
                newRandomPos = new Vector3(
                    x: Random.Range(meshCollider.bounds.extents.x - offset, -meshCollider.bounds.extents.x + offset),
                    y: 0.25f,
                    z: Random.Range(meshCollider.bounds.extents.z - offset, -meshCollider.bounds.extents.z + offset)
                    );
                
                validPosition = true;
                foreach (Vector3 pos in spawnedWallsPos)
                {
                    float distance = Vector3.Distance(pos, newRandomPos);
                    if (distance < minimumDistance)
                    {
                        validPosition = false;
                        break;
                    }
                }
            }

            GameObject newWall = Instantiate(walls[i], newRandomPos, Quaternion.Euler(0, -180, 0));
            
            Vector3 randomScale = new Vector3(
                x: Random.Range(maxScale.x, maxScale.x),
                y: Random.Range(minScale.y, maxScale.y),
                z: Random.Range(maxScale.z, maxScale.z)
            );

            newWall.transform.localScale = randomScale;
            
            spawnedWallsPos.Add(newRandomPos);
        }
        
        //fix timing issue by using Coroutine
        StartCoroutine(UpdateGridAfterWall());
    }

    IEnumerator UpdateGridAfterWall()
    {
        Debug.Log("Grid outdated");
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Grid updated");
        grid.UpdateGrid();
    }
}
