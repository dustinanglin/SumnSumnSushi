using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopstickCollisionRightSphere : MonoBehaviour {

    private ChopstickRotateOculus my_sticks;

    void Start()
    {
        string my_name = name;
        if (my_name.Contains("chR"))
            my_sticks = GameObject.Find("Chopsticks_Right").GetComponent<ChopstickRotateOculus>();
        else if (my_name.Contains("chL"))
            my_sticks = GameObject.Find("Chopsticks_Left").GetComponent<ChopstickRotateOculus>();
    }

    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Trigger with:" + collision.gameObject);

        if (collision.gameObject.GetComponent<Grabbable>() != null && my_sticks.grabbing == false)
        {
            //Debug.Log("Collided with:" + collision.gameObject.name);
            my_sticks.right_target = collision.gameObject.GetComponent<Grabbable>();
        }
        else if (collision.gameObject.GetComponentInParent<Grabbable>() != null && my_sticks.grabbing == false)
        {
            //Debug.Log("Collided with parent of:" + collision.gameObject.name);
            my_sticks.right_target = collision.gameObject.GetComponentInParent<Grabbable>();
        }
    }



    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<Grabbable>() != null && my_sticks.grabbing == false)
        {
            Debug.Log("Collided ended with:" + collision.gameObject.name);
            my_sticks.right_target = null;
        }
        else if (collision.gameObject.GetComponentInParent<Grabbable>() != null && my_sticks.grabbing == false)
        {
            //Debug.Log("Collided ended with parent of:" + collision.gameObject.name);
            my_sticks.right_target = null;
        }
    }
}

