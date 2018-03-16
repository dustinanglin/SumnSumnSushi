using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateBurst : MonoBehaviour {

	// Use this for initialization
	void Start () {
        this.GetComponent<Animation>().Play("XenoChestburst");
        Debug.Log(this.GetComponent<Animation>().IsPlaying("XenoChestburst"));

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
