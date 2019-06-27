﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenMover : MonoBehaviour {

    public bool finishedMoving, doorClosed;
    private Animator screenAnimator, doorAnimator;
    private AudioSource door_close;

	// Use this for initialization
	void Start () {
        finishedMoving = false;
        doorClosed = false;
        screenAnimator = this.GetComponent<Animator>();
        doorAnimator = GameObject.Find("HiddenDoor").GetComponent<Animator>();
        door_close = GameObject.Find("HiddenDoor").GetComponents<AudioSource>()[1];
    }
	
	// Update is called once per frame
	void Update () {
        if (screenAnimator.GetCurrentAnimatorStateInfo(0).IsName("MoveScreen") && screenAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            finishedMoving = true;
            doorClosed = false;
        }

        if (screenAnimator.GetCurrentAnimatorStateInfo(0).IsName("ReverseScreen") && screenAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            finishedMoving = false;
            if (!doorClosed)
            {
                doorClosed = true;
                doorAnimator.SetBool("OpenDoor", false);
                door_close.Play();
            }
        }
    }
}
