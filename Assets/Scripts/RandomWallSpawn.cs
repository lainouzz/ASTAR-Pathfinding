using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomWallSpawn : MonoBehaviour
{
    public Grid grid;
    public GameObject[] walls;
    
    public int wallAmount;

    public Vector3 minScale = new Vector3(1f, 1f, 1f);
    public Vector3 maxScale = new Vector3(8, 1f, 8f);
    
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
            Vector3 newRandomPos = new Vector3(Random.Range(-20f, 20f), .25f, Random.Range(-20, 20));
            
            if (i < walls.Length)
            {
                //spawn newWall
                GameObject newWall = Instantiate(walls[i], newRandomPos, Quaternion.identity);

                //random scale on x,y,z
                Vector3 randomScale = new Vector3(
                    Random.Range(minScale.x, maxScale.x),
                    Random.Range(minScale.y, maxScale.y),
                    Random.Range(minScale.z, maxScale.z));

                newWall.transform.localScale = randomScale;
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
