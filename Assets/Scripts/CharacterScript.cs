using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class CharacterScript : MonoBehaviour
{
    public Transform target;
    public float speed = 10000.0f;
    private Vector3 targetpos;
    public GameObject tower;
    private bool colliding = false;

    private void Start()
    {
        target = tower.transform;
        GetTargetPos();
    }
    private void Update()
    {
        if (!colliding)
        {
            gravity();
        }
             
    }

    public void GetTargetPos()
    {
        if (target != null)
        {
            targetpos = tower.transform.position;
            targetpos.y += tower.transform.position.y + GameObject.Find("Character").transform.localScale.y;
        }
    }

   public void PlayerMove()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetpos, step);
        if (transform.position == targetpos)
        {
            GameObject.Find("PoleCollider").GetComponent<PoleCollisionScript>().CollisionCheck = false;
        }
    }
    

    private void gravity()
    {
        transform.position.y += -9.8f * Time.deltaTime
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject != null)
        {
            colliding = true;
        }
    }
}
