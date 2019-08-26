using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTune : MonoBehaviour {

    private animate_pip pipAnimator;
    private RadioTune radioTuner;

	// Use this for initialization
	void Start () {
        pipAnimator = GetComponentInChildren<animate_pip>();
        radioTuner = GetComponentInChildren<RadioTune>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.relativeVelocity.magnitude >= 2.5f)
        {
            pipAnimator.do_animate = true;
            radioTuner.tune_new_audio = true;
        }
    }
}
