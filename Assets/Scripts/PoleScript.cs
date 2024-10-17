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
    public float extentionSpeed = 1000f;
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
    private Vector3 NewPipeVert;
    private Vector3 NextPipeVert;
    public Transform CustomPivot;

    public GameObject PipeCollider;
    
    
   






    private void Start()
    {
        //gets the current position at the start of the game for debuging.
        CurenrtPos = PipeCollider.transform.position;
        
    }

    // Update fucntion called every game tick.
    private void Update()
    {
        // calls the PoleFalling Functuion every game tick.
        Polefalling();
        // calls the PoleExtention Functuion every game tick.
        PoleExtention();
        if (PipeCollider.GetComponent<PoleCollisionScript>().CollisionCheck == true)
        {
            isRotating = false;
        }
        

    }

    
    private void Polefalling()
    {
        if (Input.GetKeyUp(KeyCode.Space))
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
        if (!isSpacePressed)
        {
            if (isExtending)
            {
                NextLength += new Vector3(0f, 1f);
                NextVert += new Vector3(0f, 0.5f);
                NewLength = new Vector3(10f, 0f) + NextLength;
                NewVert = new Vector3(250f, 415f) + NextVert;
                NextPipeVert += new Vector3(0f, 1f);
                NewPipeVert = new Vector3(250f, 415f) + NextPipeVert;

                // Changes the scale and position of the pole so it moves vertically only.
                transform.localScale = NewLength;
                transform.position = NewVert;
                PipeCollider.transform.position = NewPipeVert;

            }
        }
    }
}
