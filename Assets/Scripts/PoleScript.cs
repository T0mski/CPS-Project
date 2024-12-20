using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class PoleScript : MonoBehaviour
{
    // declares the variables.
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
    public UnityEngine.Transform CustomPivot { get; set; }
    

    public GameObject PipeCollider;
    public PoleCollisionScript poleCollisionScript;
    public GameObject PlayerCharacter;
    

    private Vector3 Scale;
    private Quaternion Rotation;
    private Vector3 Position;

    private Quaternion ColliderRot;
    private Vector3 ColliderPos;

    private bool HasReset;

    

    // Update fucntion called every game tick.
    private void Update()
    {

        
        // calls the PoleFalling Functuion every game tick.
        Polefalling();
        // calls the PoleExtention Functuion every game tick.
        PoleExtention();
        // Checks collisons 
        CollisionChecker();
   


        if (GameObject.Find("PoleCollider").GetComponent<PoleCollisionScript>().CollisionType == "NextBuilding" && !HasReset)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            PipeCollider.transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            HasReset = true;
        }

    }
    private void Start()
    {
        //Get the starting atrebutes for all of the areas of the pole.
        Scale = transform.localScale;
        Rotation = transform.rotation;
        Position = transform.position;

        ColliderPos = PipeCollider.transform.position;
        ColliderRot = PipeCollider.transform.rotation;

        CustomPivot = GameObject.Find("Pivot").transform;
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
        //Checks if the Space key is pressed.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isExtending = true;
            
        }
        // if space has not been pressed before and the pole is allowed to extend then the code below is executed.
        if (!isSpacePressed && isExtending)
        {
            
            NextLength.y += 100f * Time.deltaTime; //Multipys the time between frames by 100 and adds that to the current value of the NextLengths Y value.
            NextColliderVert.y += 100f * Time.deltaTime; // Multipys the time between frames by 100 and adds that to the current value of the Colliders next Y value
            NextVert.y += (100f / 2) * Time.deltaTime;// Multplys the time between frames by half of the standard and adds that to the new vertical height of the pole.

            NewLength = new Vector3(10f, 100f) + NextLength; // Adds the next length to the current .
            NewVert = new Vector3(200f, 465f) + NextVert; // adds the next vert to the current vertical.
            NewColliderVert = new Vector3(200f, 515f) + NextColliderVert; // adds the next collider vert to the current collider vert.

            // Changes the scale and position of the pole so it moves vertically only.
            transform.localScale = NewLength;
            transform.position = NewVert;
            PipeCollider.transform.position = NewColliderVert;

        }
    }
   
    
    //Checks collisions.
    void CollisionChecker()
    {
        if (PipeCollider.GetComponent<PoleCollisionScript>().CollisionCheck == true)
        {
            isRotating = false;
            // checks if the pole has landed on the next building correctly.
            OnBuilding();
        }
    }
    //checks if the pole has landed flat on a building.
    void OnBuilding()
    {
        if (/*gameObject.transform.rotation.z < -0.70f && gameObject.transform.rotation.z > -0.75f*/ poleCollisionScript.CollisionType == "NextBuilding" )
        {
            PlayerCharacter.GetComponent<CharacterScript>().IsMoving = true;
        }

        
    }
}
