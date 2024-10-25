using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class CharacterScript : MonoBehaviour
{
    
    public Transform target; // This stores the Target Location for the player character to go towards.
    public float speed; // speed constant
    private Vector3 targetpos; // the 3D Vector of the locatiom that you want the player to move towards.
    public GameObject tower; // the game object of the next building to get the target.
    private bool colliding = false; // Whether or not the player is colliding witht the ground.
    private Vector3 ThisObject; // A variable of the 3D Vector of this game objects location.

    // This function is called at the start of the game and only at the start of the game. 
    private void Start()
    {
        target = tower.transform;
        GetTargetPos();
    }
    //This is the function that is called every game frame.
    private void Update()
    {
        if (!colliding)
        {
            gravity();
        }
        
             
    }
    // sets the target position of the player so that it can move to the next building. 
    public void GetTargetPos()
    {
        if (target != null)
        {
            targetpos = tower.transform.position;
            targetpos.y += tower.transform.position.y + GameObject.Find("Character").transform.localScale.y;
        }
    }
    // the function that is called when the player moves towards the targetpos
   public void PlayerMove()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetpos, step);
        if (transform.position == targetpos)
        {
            GameObject.Find("PoleCollider").GetComponent<PoleCollisionScript>().CollisionCheck = false;
        }
    }
    
    // moves the player down by a specific amount of pixels every frame. Acts as gravity.
    private void gravity()
    {
        ThisObject = transform.position;
        ThisObject.y += -9.8f * Time.deltaTime;
        transform.position = ThisObject;
    }
    //Checks the collisions between the two game objects.
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject != null)
        {
            colliding = true;
        }
    }
}
