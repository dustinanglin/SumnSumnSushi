using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XenoAnimate : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<Animation>()["Chesburst"].speed = .5f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
