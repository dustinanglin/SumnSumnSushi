﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerScreen : MonoBehaviour {

    private GameObject orderScreen;
    private Animator screenAnimator, doorAnimator;
    private AudioSource screen_move;
    private bool doorMoved, buttonsRefreshed;

	// Use this for initialization
	void Start () {
        orderScreen = GameObject.Find("VendingScreen");
        screenAnimator = orderScreen.GetComponent<Animator>();
        doorAnimator = this.GetComponent<Animator>();
        screen_move = orderScreen.GetComponentsInChildren<AudioSource>()[0];

        doorMoved = false;
        buttonsRefreshed = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("OpenDoor") && doorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && !doorMoved)
        {
            screenAnimator.SetBool("DoScreenMove", true);
            doorMoved = true;
            buttonsRefreshed = false;
            screen_move.Play();
            //Debug.Log("Move Screen!");
        }

        if (doorAnimator.GetCurrentAnimatorStateInfo(0).IsName("CloseDoor") && doorAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
            doorMoved = false;
            if (!buttonsRefreshed)
            {
                RefreshButtons();
                buttonsRefreshed = true;
            }
            //Debug.Log("Move Screen!");
        }

    }

    private void RefreshButtons()
    {
        int i;
        for (i = 1; i <= 9; i++)
        {
            string buttonName = "Button" + i;
            string buttonHolder = "Button" + i + "Holder";
            if (GameObject.Find(buttonHolder))
                GameObject.Find(buttonHolder).GetComponentInChildren<VendingButton>().pushed = false;
            if (GameObject.Find(buttonName))
                GameObject.Find(buttonName).GetComponent<VendingButton>().pushed = false;
            Animator buttonAnim = GameObject.Find(buttonHolder).GetComponent<Animator>();
            buttonAnim.SetBool("ButtonPress", false);
        }
    }
}
