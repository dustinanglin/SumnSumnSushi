using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDelay : MonoBehaviour {

    public float door_delay = 0f;
    private float time = 0f;
    private AnimationState myClip;

	// Use this for initialization
	void Start () {
        Animation myAnim = this.GetComponent<Animation>();
        myClip = myAnim["DoorSlam"];
        myClip.speed = 0;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= door_delay)
        {
            myClip.speed = 1;
        }

	}
}
