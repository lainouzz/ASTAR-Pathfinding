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
    public float distance;
        
    // Start is called before the first frame update
    void Start()
    {
        SpawnAtRandomPos();
    }

    public void SpawnAtRandomPos()
    {
        for (int i = 0; i < wallAmount; i++)
        {
            //random position x,z
            Vector3 newRandomPos = new Vector3(Random.Range(meshCollider.bounds.extents.x - offset, -meshCollider.bounds.extents.x + offset),
                .25f, Random.Range(meshCollider.bounds.extents.z - offset, -meshCollider.bounds.extents.z + offset));
            
            if (i < walls.Length)
            {
                distance = Vector3.Distance(walls[i].transform.position, startPosition.position);
                
                if (distance > 4f)
                {
                    GameObject newWall = Instantiate(walls[i], newRandomPos, Quaternion.identity);
                    
                    //random scale on x,y,z
                    Vector3 randomScale = new Vector3(
                        Random.Range(maxScale.x, maxScale.x),
                        Random.Range(minScale.y, maxScale.y),
                        Random.Range(maxScale.z, maxScale.z));

                    newWall.transform.localScale = randomScale;
                    
                    Debug.Log("wall name: " + walls[i].name);
                }
                else
                {
                    return;
                }
            }
            else
            {
                Debug.LogWarning("Index " + i + " exceeds the walls array length.");
            }
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
