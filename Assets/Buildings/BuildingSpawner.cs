using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;

public class BuildingSpawner : MonoBehaviour
{
    public GameObject Building;
    public float spawnrate = 2;
    private float timer = 0;

    private void Start()
    {
        SpawnBuilding();
    }
    private void Update()
    {
        if (timer < spawnrate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            SpawnBuilding();
            timer = 0;
        }
        
    }


    void SpawnBuilding()
    {
        Instantiate(Building, transform.position, transform.rotation);
    }
}
