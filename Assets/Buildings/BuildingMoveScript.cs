using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMoveScript : MonoBehaviour
{
    
    private GameObject building;
    private Vector3 ThisObject;
    private void Update()
    {
        ReverseGravity();
    }
    private void ReverseGravity()
    {
        ThisObject = transform.position;
        ThisObject.y += 9.8f * Time.deltaTime;
        transform.position = ThisObject;
    }

      



}
