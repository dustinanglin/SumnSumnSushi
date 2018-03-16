using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopstickShadowRotate : MonoBehaviour {


    private Vector3 pivot_point;
    private GameObject left_chopstick;
    private GameObject right_chopstick;
    private GameObject right_actual, left_actual;
    private OVRInput.Controller current_controller;
    private float rotate_angle;
    public float max_angle;
    public float min_angle;
    public float chopstick_speed;
    public bool left_colliding;
    public bool right_colliding;
    public bool grabbing = false;
    public Grabbable grab_target;
    public Grabbable left_target;
    public Grabbable right_target;
    private GameObject hand_anchor;

    // Use this for initialization
    void Start()
    {
        left_chopstick = this.transform.Find("Left").gameObject;
        right_chopstick = this.transform.Find("Right").gameObject;
        
        max_angle = 20;

        switch (this.name)
        {
            case "Chopsticks_Left_Shadow":
                current_controller = OVRInput.Controller.LTouch;
                hand_anchor = GameObject.Find("ChopstickTargetLeft");
                right_actual = GameObject.Find("Chopsticks_Left").transform.Find("Right_chL").gameObject;
                left_actual = GameObject.Find("Chopsticks_Left").transform.Find("Left_chL").gameObject;
                break;
            case "Chopsticks_Right_Shadow":
                current_controller = OVRInput.Controller.RTouch;
                hand_anchor = GameObject.Find("ChopstickTargetRight");
                right_actual = GameObject.Find("Chopsticks_Right").transform.Find("Right_chR").gameObject;
                left_actual = GameObject.Find("Chopsticks_Right").transform.Find("Left_chR").gameObject;
                break;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Debug.Log("Left:" + left_colliding + " Right:" + right_colliding);
        //Debug.Log(grab_target);

        MoveChopsticks();
        Rotate();

    }

    void Rotate()
    {
        //rotate_angle = Mathf.Clamp(14 * OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, current_controller) - 6, -10, max_angle);
        //right_chopstick.transform.localEulerAngles = new Vector3(0, 0, rotate_angle);
        right_chopstick.GetComponent<Rigidbody>().MoveRotation(right_actual.transform.rotation);
        left_chopstick.GetComponent<Rigidbody>().MoveRotation(left_actual.transform.rotation);
        /*Debug.Log("Rbody:" + right_chopstick.GetComponent<Rigidbody>().transform.rotation);
        Debug.Log(right_actual.transform.rotation);*/
    }

    void MoveChopsticks()
    {
        right_chopstick.GetComponent<Rigidbody>().MovePosition(right_actual.transform.position);
        left_chopstick.GetComponent<Rigidbody>().MovePosition(left_actual.transform.position);
        //this.GetComponent<Rigidbody>().MovePosition(hand_anchor.transform.position);
        //this.GetComponent<Rigidbody>().MoveRotation(hand_anchor.transform.rotation);
        //this.transform.position = hand_anchor.transform.position;
        //this.transform.rotation = hand_anchor.transform.rotation;
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Grabbable>() != null)
        {
            Debug.Log("Collided with:" + collision.gameObject.name);
            grab_target = collision.gameObject.GetComponent<Grabbable>();
        }
    }*/
}