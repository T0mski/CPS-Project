using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private float fallspeed = 100f;

    // Update is called once per frame
    void Update()
    {
        var PoleCollision = GameObject.FindWithTag("Pole");

        float orientation = PoleCollision.transform.localRotation.z;

        float newOrientation = PoleFall(orientation);

        PoleCollision.transform.localEulerAngles = new Vector2(0f , newOrientation);
    }




    private float PoleFall(float orientation)
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            float neworientation = (orientation + 1) * fallspeed;
            Debug.Log(neworientation);
            return neworientation;
        }
        return 0f;
    }
}
