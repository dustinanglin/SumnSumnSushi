using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopstickRotateOculus : MonoBehaviour
{

    private Vector3 pivot_point;
    private GameObject left_chopstick;
    private GameObject right_chopstick;
    public OVRInput.Controller current_controller;
    private float rotate_angle;
    public float max_angle;
    public float min_angle;
    public float chopstick_speed;
    public bool left_colliding;
    public bool right_colliding;
    public bool grabbing = false;
    public bool cant_grab = false;
    public Grabbable grab_target;
    public Grabbable left_target;
    public Grabbable right_target;
    public GameObject hand_anchor;
    private ChopstickShadowRotate my_shadow;

    // Use this for initialization
    void Start()
    {
        
        left_colliding = false;
        right_colliding = false;
        max_angle = 20;

        switch (this.name)
        {
            case "Chopsticks_Left":
                current_controller = OVRInput.Controller.LTouch;
                hand_anchor = GameObject.Find("ChopstickTargetLeft");
                left_chopstick = this.transform.Find("Left_chL").gameObject;
                right_chopstick = this.transform.Find("Right_chL").gameObject;
                my_shadow = GameObject.Find("Chopsticks_Left_Shadow").GetComponent<ChopstickShadowRotate>();
                break;
            case "Chopsticks_Right":
                current_controller = OVRInput.Controller.RTouch;
                hand_anchor = GameObject.Find("ChopstickTargetRight");
                left_chopstick = this.transform.Find("Left_chR").gameObject;
                right_chopstick = this.transform.Find("Right_chR").gameObject;
                my_shadow = GameObject.Find("Chopsticks_Right_Shadow").GetComponent<ChopstickShadowRotate>();
                break;
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Debug.Log("Left:" + left_colliding + " Right:" + right_colliding);
        //Debug.Log(grab_target);

        MoveChopsticks();

        if (left_target != null && right_target != null && rotate_angle > -6 && !grabbing && !cant_grab)
        {
            if (left_target == right_target)
            {
                grab_target = left_target;
                max_angle = rotate_angle;
                my_shadow.max_angle = max_angle;
            }
        }

        if (grab_target != null && !grabbing)
        {
            grabbing = true;
            grab_target.GrabBegin(hand_anchor, this.gameObject);
            Debug.Log("Begin grab");
            //max_angle = rotate_angle;
        }
        else if (rotate_angle <= -1 && grabbing)
        {
            grabbing = false;
            grab_target.GrabEnd();
            Debug.Log("Begin end");
            grab_target = null;
            left_target = null;
            right_target = null;
        }

        if (rotate_angle <= -1)
        {
            max_angle = 20;
            my_shadow.max_angle = max_angle;
        }

        if (rotate_angle >= 8 && grab_target == null)
        {
            cant_grab = true;
        }

        if (cant_grab && rotate_angle < 8)
            cant_grab = false;

        Rotate();

    }

    void Rotate()
    {
        rotate_angle = Mathf.Clamp(14 * OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, current_controller) - 6,-10,max_angle);
        right_chopstick.transform.localEulerAngles = new Vector3(0, 0, rotate_angle);
    }

    void MoveChopsticks()
    {
        /*this.GetComponent<Rigidbody>().MovePosition(hand_anchor.transform.position);
        this.GetComponent<Rigidbody>().MoveRotation(hand_anchor.transform.rotation);*/

        this.transform.position = hand_anchor.transform.position;
        this.transform.rotation = hand_anchor.transform.rotation;
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
