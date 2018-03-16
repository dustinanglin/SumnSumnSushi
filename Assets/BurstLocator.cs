using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurstLocator : MonoBehaviour {
    private Transform head_location;
	// Use this for initialization
	void Start () {
        head_location = GameObject.Find("CenterEyeAnchor").transform;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.position = head_location.transform.position;
	}
}
