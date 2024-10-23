using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UI;
using UnityEngine;

public class PoleScript : MonoBehaviour
{
    // declares the variables
    public float rotationSpeed = 100f;
    public float extentionSpeed = 0.01f;
    public bool isRotating = false;  
    private bool isExtending = false;
    private bool isSpacePressed = false;
    
    // declares all the variables for the extention of the pole.
    private Vector3 NextLength;
    private Vector3 NewLength;
    private Vector3 origionalSize;
    private Vector3 NextVert;
    private Vector3 NewVert;
    private Vector3 CurenrtPos;
    private Vector3 NewColliderVert;
    private Vector3 NextColliderVert;
    public Transform CustomPivot;

    public GameObject PipeCollider;
    public GameObject PlayerCharacter;
    


 

    // Update fucntion called every game tick.
    private void Update()
    {
        // calls the PoleFalling Functuion every game tick.
        Polefalling();
        // calls the PoleExtention Functuion every game tick.
        PoleExtention();
        // Checks collisons 
        CollisionChecker();
       
        
        
    }

    private void Polefalling()
    {
        if (Input.GetKeyUp(KeyCode.Space) && !isSpacePressed)
        {
            isRotating = true;
            isExtending = false;
            isSpacePressed = true;
        }

        if (isRotating)
        {
            transform.RotateAround(CustomPivot.position, Vector3.back, rotationSpeed * Time.deltaTime);
            PipeCollider.transform.RotateAround(CustomPivot.position, Vector3.back, rotationSpeed * Time.deltaTime);
        }
    }

    private void PoleExtention()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isExtending = true;
        }
        if (!isSpacePressed && isExtending)
        {

            NextLength.y += 100f * Time.deltaTime;
            NextColliderVert.y += 100f * Time.deltaTime;
            NextVert.y += (100f / 2) * Time.deltaTime;

            NewLength = new Vector3(10f, 100f) + NextLength;
            NewVert = new Vector3(200f, 465f) + NextVert;
            NewColliderVert = new Vector3(200f, 515f) + NextColliderVert;

            // Changes the scale and position of the pole so it moves vertically only.
            transform.localScale = NewLength;
            transform.position = NewVert;
            PipeCollider.transform.position = NewColliderVert;

          
        }
    }

    void CollisionChecker()
    {
        if (PipeCollider.GetComponent<PoleCollisionScript>().CollisionCheck == true)
        {
            isRotating = false;
            // checks if the pole has landed on the next building correctly.
            OnBuilding();
        }
    }

    void OnBuilding()
    {
        if (gameObject.transform.rotation.z < -0.70f && gameObject.transform.rotation.z > -0.75f)
        { 
            PlayerCharacter.GetComponent<CharacterScript>().PlayerMove();

        }

        else
        {
            
        }
    }
}
