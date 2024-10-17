using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PoleCollisionScript : MonoBehaviour
{

    public bool CollisionCheck = false;
    public Collider2D PoleCollider;

   


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Building")
        {
            CollisionCheck = true;
            Debug.Log("Is Colliding ");
        
        }
    }
} 
