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
    public GameObject Character;
    public GameObject Target;
    public GameObject Player;

    public GameObject Pivot;

    private GameObject CurrentBuilding;

    private bool DoneOnce = true;

    public float Deadzone = -10f;

   

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
        GameObject MainCamera = GameObject.Find("Main Camera");
        GameObject Spawner = GameObject.Find("Spawner");
        float PivotX = Pivot.transform.position.x;
        float SpawnerX = Spawner.transform.position.x;
        float MainCameraX = MainCamera.transform.position.x;
        PivotX += 400f;
        SpawnerX += 400f;
        MainCameraX += 400f;
        Pivot.transform.position = new Vector3(PivotX, Pivot.transform.position.y, Pivot.transform.position.z);
        Spawner.transform.position = new Vector3(SpawnerX, Spawner.transform.position.y, Spawner.transform.position.z);
        MainCamera.transform.position = new Vector3(MainCameraX, MainCamera.transform.position.y, MainCamera.transform.position.z);
    }
    


    private void Update()
    {
        if (Character.transform.position.x == Tower.transform.position.x && DoneOnce)
        {
            MoveAll();
            SpawnBuilding();
            OnBecameInvisible();
            DoneOnce = false;
            GameObject.Find("Pole").GetComponent<PoleScript>().Restart();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentBuilding = GameObject.Find("What is below.").GetComponent<whatisBelow>().other;
            CurrentBuilding.name = "PreviousBuilding";
        }
        
    }
    void OnBecameInvisible()

    {
        Destroy(GameObject.Find("PreviousBuilding"));
    }
}

