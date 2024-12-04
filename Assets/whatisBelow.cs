using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class whatisBelow : MonoBehaviour
{
    public GameObject below = null ;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NextBuilding"))
        {
           below = collision.gameObject;
           Debug.Log("It is colliding with something so the code should work");

        }

        else
        {
            Debug.Log("Something Else");
        }
    }
}
