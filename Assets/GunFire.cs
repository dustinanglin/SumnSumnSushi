using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFire : MonoBehaviour {

    private Animation gunAnimation;
    //private AudioSource gun_shot;
    private AnimationState gunState;
    private OVRInput.Controller current_controller;
    private TimeManipulator timeMan;
    private float trigger_down;
    public float fire_speed = 1;
    private bool fired = false;


	// Use this for initialization
	void Start () {
        gunAnimation = this.GetComponentInChildren<Animation>();
        //gun_shot = GetComponent<AudioSource>();
        timeMan = GameObject.Find("TimeController").GetComponent<TimeManipulator>();
        gunState = gunAnimation["Gunfire"];
        if (name.Contains("Left"))
            current_controller = OVRInput.Controller.LTouch;
        else
            current_controller = OVRInput.Controller.RTouch;
        
	}
	
	// Update is called once per frame
	void Update () {
        trigger_down = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, current_controller);
        if (trigger_down <= .9f && !fired)
        {
            gunState.time = trigger_down;
            gunState.speed = 0;
            gunAnimation.Play();
        }
        if (trigger_down > .9f && !fired)
        {
            fired = true;
            gunState.time = .9f;
            gunState.speed = fire_speed;
            gunAnimation.Play();
            GetComponentInChildren<BulletGenerator>().Fire();
            //gun_shot.Play();
            timeMan.FireBoost();
        }
        if (fired && trigger_down <= .8f)
        {
            fired = false;
        }
	}
}
