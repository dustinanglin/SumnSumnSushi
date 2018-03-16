using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAnimate : MonoBehaviour {

    private AnimationState myClip;       

	// Use this for initialization
	void Start () {

        myClip = this.GetComponent<Animation>()["Wave"];
        myClip.wrapMode = WrapMode.Loop;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
