using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChopstickCollision : MonoBehaviour {

    private ChopstickRotateOculus my_sticks;

    private void Start()
    {
        switch (name) {
            case "CollisionSphere_Right":
                my_sticks = GameObject.Find("Chopsticks_Right").GetComponent<ChopstickRotateOculus>();
                break;
            case "CollisionSphere_Left":
                my_sticks = GameObject.Find("Chopsticks_Left").GetComponent<ChopstickRotateOculus>();
                break;

            }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Grabbable>() != null && my_sticks.grabbing == false)
        {
            //Debug.Log("Collided with:" + collision.gameObject.name);
            my_sticks.grab_target = collision.gameObject.GetComponent<Grabbable>();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<Grabbable>() != null && my_sticks.grabbing == false)
        {
            //Debug.Log("Collided ended with:" + collision.gameObject.name);
            my_sticks.grab_target = null;
        }
    }
}
