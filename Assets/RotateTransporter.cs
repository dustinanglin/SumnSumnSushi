using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTransporter : MonoBehaviour {
    public float rotate_speed = 0f;

	// Use this for initialization
	void Start () {
        transform.position = (GameObject.Find("CenterEyeAnchor").transform.position) + new Vector3(0, 1, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<ParticleSystem>().isPlaying)
        {
            transform.Rotate(0, 0, rotate_speed);
            transform.position = (GameObject.Find("CenterEyeAnchor").transform.position) + new Vector3(0, 1, 0);
        }


	}
}
