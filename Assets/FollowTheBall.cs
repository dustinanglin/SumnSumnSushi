using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTheBall : MonoBehaviour {

    private GameObject the_sphere;
    public Vector3 offset;

	// Use this for initialization
	void Start () {

        the_sphere = GameObject.Find("Sphere");
        
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = the_sphere.transform.position + offset;
	}
}
