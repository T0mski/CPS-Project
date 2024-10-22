using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class CharacterScript : MonoBehaviour
{
    public Transform target;
    public float speed = 10000.0f;
    private Vector3 targetpos;
    public GameObject tower;

    private void Start()
    {
        target = tower.transform;
    }

    public void GetTargetPos()
    {
        if (target != null)
        {
            targetpos = transform.position + (tower.transform.localPosition/2);
        }
    }

   public void PlayerMove()
    {
        float step = speed * Time.deltaTime;
        Debug.Log(step);
        Debug.Log(Time.deltaTime);
        Debug.Log(speed);
        
        transform.position = Vector3.MoveTowards(transform.position, targetpos, step);
    }
}
