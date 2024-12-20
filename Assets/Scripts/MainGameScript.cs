using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject CurrentCollider;

    private bool DoneOnce = false;
    private bool DoOnce = false;

    public float Deadzone = -10f;
    public GameObject QuizManager;
    
    public CharacterScript characterScript;
    [SerializeField]
    private IntSO scoreSO; 

    void Update()
    {
        // once the player has made it to the next building it will "Reload the game" (starts again)
        // also adds score to the score card.
        if (Character.transform.position.x == Tower.transform.position.x && !DoneOnce)
        { 

            DoneOnce = true;
            SceneManager.LoadScene("MainGame");
            AddScore();
        }
        // Sets different atributes of the object below the player .
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentBuilding = GameObject.Find("What is below.").GetComponent<whatisBelow>().other;
            CurrentBuilding.name = "PreviousBuilding";
            CurrentCollider.tag = "Building";
        }
        // Check if the player has fallen down to show the QuizManager so the player
        // can answer the question.
        if (scoreSO.Value >= 0 && characterScript.HasFallenDown)
        {
            QuizManager.SetActive(true); 
        }

    }

    private void AddScore()
    {
        if (!DoOnce)
        {
            scoreSO.Value += 1;
            DoOnce = true;
        }
    }


    private void Start()
    {
      QuizManager.SetActive(false);
    }
}

