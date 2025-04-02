using UnityEngine;

public class whatisBelow : MonoBehaviour
{
    public GameObject other { get; set; }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NextBuilding"))
        {
            other = collision.transform.parent.gameObject;


        }
        else
        {

        }
    }
}
