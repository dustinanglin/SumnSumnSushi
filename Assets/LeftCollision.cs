using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftCollision : MonoBehaviour {

    void OnTriggerEnter(Collider collision)
    {
        ChopstickRotateOculus temp = GameObject.Find("Chopsticks_Right").GetComponent<ChopstickRotateOculus>();
        if (collision.gameObject.GetComponent<Grabbable>() != null && temp.grabbing == false)
        {
            //Debug.Log("Collided with:" + collision.gameObject.name);
            temp.left_target = collision.gameObject.GetComponent<Grabbable>();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        ChopstickRotateOculus temp = GameObject.Find("Chopsticks_Right").GetComponent<ChopstickRotateOculus>();
        if (collision.gameObject.GetComponent<Grabbable>() != null && temp.grabbing == false)
        {
            //Debug.Log("Collided ended with:" + collision.gameObject.name);
            temp.left_target = null;
        }
    }
}
