using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMoveScript : MonoBehaviour
{
    public float movespeed = 20f;


    private void Update()
    {
         transform.position = transform.position + ((Vector3.left * movespeed) * Time.deltaTime);
    }


      



}
