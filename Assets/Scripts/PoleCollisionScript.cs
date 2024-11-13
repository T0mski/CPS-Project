using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
            Character.GetComponent<CharacterScript>().AllowedToFall = false;
            Debug.Log(Character.GetComponent<CharacterScript>().AllowedToFall);

        }
        else if (other.gameObject.CompareTag("Building"))
        {
            CollisionCheck = true;
            CollisionType = other.gameObject.tag;
            Character.GetComponent<CharacterScript>().AllowedToFall = true;
            Debug.Log(Character.GetComponent<CharacterScript>().AllowedToFall);
        }
    }
} 