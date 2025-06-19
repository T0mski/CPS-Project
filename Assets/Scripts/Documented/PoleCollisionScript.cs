using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class PoleCollisionScript : MonoBehaviour
{

    public bool CollisionCheck = false;
    public string CollisionType;
    public GameObject Character;
   // Called when a collider2D hits another Collider2D in the game tick (called once).
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Called when the collider lands on the correct building.
        if (other.gameObject.CompareTag("NextBuilding"))
        {
            CollisionCheck = true;
            CollisionType = other.gameObject.tag;
        }
        // Called when the collider lands on the correct building.
        else if (other.gameObject.CompareTag("Building"))
        {
            CollisionCheck = true;
            CollisionType = other.gameObject.tag;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CollisionCheck = false;
        CollisionType = null; 
    }
} 