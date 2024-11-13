using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    private bool HasFallenDown = false;
    public bool IsMoving;
    public bool AllowedToFall;

    // This function is called at the start of the game and only at the start of the game. 
    private void Start()
    {
        target = tower.transform;
        GetTargetPos();
       
        
    }
    //This is the function that is called every game frame.
    private void Update()
    {
        if (AllowedToFall)
        {
            gravity();
        }

        HasFallen();
        PlayerMove();
        Debug.Log(IsMoving);
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
        if (IsMoving)
        {
            float step = speed * Time.deltaTime;
            float targetposX = targetpos.x;
            float currentposX = transform.position.x;
            float currentposY = transform.position.y;
            /*transform.position = Vector3.MoveTowards(transform.position, targetpos, step); The unity MoveTowards Command Didnt allow me to do what i wanted so i crated the 
            MoveTo command to only move the X value*/
            float nextX = MoveTo(currentposX, targetposX, step);
            if (!HasFallenDown)
            {
                transform.position = new Vector3(nextX, currentposY);
                if (transform.position == targetpos)
                {
                    GameObject.Find("PoleCollider").GetComponent<PoleCollisionScript>().CollisionCheck = false;
                }

            }

            if (transform.position.x == targetpos.x)
            {
                IsMoving = false;
            }

        }
        
    }
    
    // moves the player down by a specific amount of pixels every frame. Acts as gravity.
    public void gravity()
    {
        if (!colliding)
        {
            ThisObject = transform.position;
            ThisObject.y += (2 * -9.8f) * Time.deltaTime;
            transform.position = ThisObject;
        }
    }
    //Checks the collisions between the two game objects.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject != null)
        {
            colliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject != null)
        {
            colliding = false;
        }
    }
    // My adaptation of the Unity Vector3.MoveTowards function but only affects 1 direction.
    private float MoveTo(float CurrentPos, float TargetPos, float MaxStep)
    {
        float num = TargetPos - CurrentPos;
        float num2 = num * num;

        if (num2 == 0f || (MaxStep >= 0f && num2 <= MaxStep * MaxStep))
        {
            return TargetPos;
        }

        float num3 = (float)Math.Sqrt(num2);
        return (CurrentPos + num / num3 * MaxStep);
    }

    private void HasFallen()
    {
        if (transform.position.y < 450f && GameObject.Find("PoleCollider").GetComponent<PoleCollisionScript>().CollisionType == "Building")
        {
            HasFallenDown = true;
        }
        
    }
}
