using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shownormals : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, transform.up, Color.cyan);
	}
}
