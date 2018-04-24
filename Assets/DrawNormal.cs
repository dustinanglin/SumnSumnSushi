using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawNormal : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (gameObject.name.Contains("Plane"))
            Debug.DrawRay(transform.position, transform.up, Color.red);
        else
            Debug.DrawRay(transform.position, transform.right, Color.red);
    }
}
