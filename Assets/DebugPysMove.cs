using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPysMove : MonoBehaviour {

    public float move_speed = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //GetComponent<Rigidbody>().velocity = new Vector3(move_speed, 0, 0);
        GetComponent<Rigidbody>().MovePosition(transform.position + new Vector3(move_speed, 0,0 ));
	}
}
