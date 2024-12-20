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
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("NextBuilding"))
        {
            CollisionCheck = true;
            CollisionType = other.gameObject.tag;
            Debug.Log(CollisionType);
            Character.GetComponent<CharacterScript>().AllowedToFall = false;
        }
        else if (other.gameObject.CompareTag("Building"))
        {
            CollisionCheck = true;
            CollisionType = other.gameObject.tag;
            Debug.Log(CollisionType);
            Character.GetComponent<CharacterScript>().AllowedToFall = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        CollisionCheck = false;
        CollisionType = null; 
    }
} 