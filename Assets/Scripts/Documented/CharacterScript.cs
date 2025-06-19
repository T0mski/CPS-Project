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

    public bool HasFallenDown = false;
    public bool IsMoving;
 
    [SerializeField]
    private BoolSO PausedSO;
    // This function is called at the start of the game and only at the start of the game. 
    private void Start()
    {
        target = tower.transform;
        GetTargetPos();
       
        
    }
    //This is the function that is called every game frame.
    private void Update()
    {
        HasFallen();
        PlayerMove();
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
            /*transform.position = Vector3.MoveTowards(transform.position, targetpos, step); The unity MoveTowards 
             * Command Didnt allow me to do what i wanted so i crated the MoveTo command to only move the X value*/
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
    
    // My adaptation of the Unity Vector3.MoveTowards function but only affects 1 direction.
    private float MoveTo(float CurrentPos, float TargetPos, float MaxStep)
    {
        float Diff = TargetPos - CurrentPos; // Gets the difference between the target and the current position/
        float DiffSqurd = Diff * Diff; // squares the difference.

        if (DiffSqurd == 0f || (MaxStep >= 0f && DiffSqurd <= MaxStep * MaxStep))// if the sqare if the difference is 0 or diff squared is less or equal to max
                                                                                 // step squared return the target
        {
            return TargetPos;
        }
            
        return (CurrentPos + Diff / Diff * MaxStep); // else return the current position + the difference ver the differecne x max step.
    }

    private void HasFallen()
    {
        if (GameObject.Find("PoleCollider").GetComponent<PoleCollisionScript>().CollisionType == "Building")
        {
            HasFallenDown = true;
            
        }
        
    }


}
