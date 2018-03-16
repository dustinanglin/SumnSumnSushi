using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boatmove : MonoBehaviour {

    public float moveSpeed = 0.05f;
    Vector3 location, init_location;
    public float time = 0;
    private float move_time = 0;
    public float tipsy_angle;

	// Use this for initialization
	void Start () {
        location = transform.position;
        init_location = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        location = init_location + new Vector3(move_time * moveSpeed, 0, 0);
        time += Time.deltaTime;
        move_time += Time.deltaTime;

        tipsy_angle = Mathf.Cos(time) * 15f;
        //Debug.Log("Time:" + time + " Postion:" + location.x);
  
        //Debug.Log(tipsy_angle);

        Vector3 tipsy_rotate = new Vector3(tipsy_angle, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        //Debug.Log(transform.rotation.eulerAngles);
        //Debug.Log(tipsy_rotate);

        this.GetComponent<Rigidbody>().MovePosition(location);
        this.GetComponent<Rigidbody>().MoveRotation(Quaternion.Euler(tipsy_rotate));
    }
}
