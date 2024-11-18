using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class MainGameScript : MonoBehaviour
{
    public GameObject Tower;
    private float[] nextMult = new float[7] { 1f, 1.5f, 0.3f, 0.5f, 1.3f, 1.8f, 0.7f };
    private int RandomNum;
    private float Multiplyer;
    private float nextScaleX;
    public GameObject Character;
    public GameObject Target;
    public GameObject Player;

    private bool DoneOnce = true;

    void SpawnBuilding()
    {
        RandomNum = Random.Range(0, nextMult.Length);
        Multiplyer = nextMult[RandomNum];

        Vector3 scale = Tower.transform.localScale;
        scale.x = scale.x * Multiplyer;
        transform.localScale = scale;
        Instantiate(Tower, transform.position, transform.rotation);
    }

    private void MoveAll()
    {
        Player.GetComponentInChildren<PoleScript>().ResetPoleToPlayer();
        /*
            Vector3 player = Player.transform.position;
        player.x -= 10f;
        Player.transform.position = player;
        */
    }
    


    private void Update()
    {
        if (Character.transform.position.x == Tower.transform.position.x && DoneOnce)
        {
            MoveAll();
            SpawnBuilding();
            DoneOnce = false;
        }
       

       
    }

}

