using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEngine;

public class MainGameScript : MonoBehaviour
{
    public GameObject Building;
    private float[] nextMult = new float[7]  {1f, 1.5f, 0.3f, 0.5f, 1.3f, 1.8f, 0.7f};
    private int RandomNum;
    private float Multiplyer;
    private float nextScaleX;

    void SpawnBuilding()
    {
        RandomNum = Random.Range(1, nextMult.Length);
        Multiplyer = nextMult[RandomNum];
        
        Vector3 scale = Building.transform.localScale;
        scale.x = scale.x * Multiplyer;
        transform.localScale = scale;
        Instantiate(Building, transform.position, transform.rotation);
    }
}