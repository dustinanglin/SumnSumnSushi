using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachBody : MonoBehaviour {

    public Transform head;
    public float rotate_tolerance, lerp_speed;
    private Vector2 head_forward, body_forward;
    public bool turn_body;
    private Quaternion look_direction;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        head_forward = new Vector2(head.forward.x, head.forward.z);
        body_forward = new Vector2(transform.forward.x, transform.forward.z);

        Debug.DrawRay(transform.position, new Vector3(body_forward.x, 0, body_forward.y));
        Debug.DrawRay(head.position, new Vector3(head_forward.x, 0, head_forward.y));

        transform.position = new Vector3(head.position.x, transform.position.y, head.position.z);

        //Debug.Log("Angle between:" + Vector2.Angle(head_forward, body_forward));

        if (turn_body)
        {
            float angle_between = Vector2.Angle(head_forward, body_forward);
            Quaternion new_forward_direction = Quaternion.LookRotation(new Vector3(head_forward.x, 0, head_forward.y), Vector3.up);

            if (angle_between < rotate_tolerance)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, new_forward_direction, lerp_speed);
            }
            else
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, new_forward_direction, lerp_speed * 10);
            }
        }
 


       /* if (Vector2.Angle(head_forward,body_forward) > rotate_tolerance && !turn_body)
        {
            turn_body = true;
        }

        if (turn_body && Vector2.Angle(head_forward, body_forward) > 1)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(new Vector3(head_forward.x, 0, head_forward.y),Vector3.up), lerp_speed);
        }
        else
        {
            turn_body = false;
            lerp_t = 0;
        }*/


    }
}
