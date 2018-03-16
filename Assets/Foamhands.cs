using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foamhands : MonoBehaviour {

    public GameObject hifive, no1, shooter, fist, thumbsup;
    private bool b_hifive, b_no1, b_shooter, b_fist, b_thumbsup, top_touch;
    private OVRInput.Controller current_controller;
    private string hand_state;

    // Use this for initialization
    void Start () {
        if (this.name.Contains("Left"))
        {
            current_controller = OVRInput.Controller.LTouch;
        }
        if (this.name.Contains("Right"))
        {
            current_controller = OVRInput.Controller.RTouch;
        }

        hand_state = null;
    }
	
	// Update is called once per frame
	void Update () {
        //fist
        top_touch = OVRInput.Get(OVRInput.Touch.PrimaryThumbRest, current_controller) || OVRInput.Get(OVRInput.Touch.One, current_controller) || OVRInput.Get(OVRInput.Touch.Two, current_controller) || OVRInput.Get(OVRInput.Touch.PrimaryThumbstick)
            || OVRInput.Get(OVRInput.NearTouch.PrimaryThumbButtons, current_controller);


        b_fist = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger,current_controller) >= .6f && OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, current_controller) && top_touch;
        fist.SetActive(b_fist);

        b_no1 = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, current_controller) >= .6f && !OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, current_controller) && top_touch;
        no1.SetActive(b_no1);

        b_thumbsup = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, current_controller) >= .6f && OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, current_controller) && !top_touch;
        thumbsup.SetActive(b_thumbsup);

        b_shooter = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, current_controller) >= .6f && !OVRInput.Get(OVRInput.Touch.PrimaryIndexTrigger, current_controller) && !top_touch;
        shooter.SetActive(b_shooter);

        b_hifive = !b_fist && !b_no1 && !b_thumbsup && !b_shooter;
        hifive.SetActive(b_hifive);
       
	}
}
