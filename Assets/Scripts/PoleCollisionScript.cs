using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PoleCollisionScript : MonoBehaviour
{

    public bool CollisionCheck = false;
    public string CollisionType;
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NextBuilding"))
        {
            CollisionCheck = true;
            CollisionType = other.gameObject.tag;

        }
        else if (other.gameObject.CompareTag("Building"))
        {
            CollisionCheck = true;
            CollisionType = other.gameObject.tag;
        }
    }
} 


