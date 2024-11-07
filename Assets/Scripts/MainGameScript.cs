using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameScript : MonoBehaviour
{
    public GameObject Building;
    


    void SpawnBuilding()
    {
        Instantiate(Building, transform.position, transform.rotation);
    }
}
