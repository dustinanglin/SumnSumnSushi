using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimator : MonoBehaviour {

    private Animation doorAnimation;
    public float od, cd;

    private float open_delay, close_delay;

    private bool animate_open, animate_close, animating;

	// Use this for initialization
	void Start () {
        animate_open = false;
        animate_close = false;
        animating = false;

        doorAnimation = this.GetComponent<Animation>();

        open_delay = od;
        close_delay = cd;
	}
	
	// Update is called once per frame
	void Update () {
		if (animate_open && open_delay > 0)
        {
            open_delay -= Time.deltaTime;
            //Debug.Log(open_delay);
        }

        if (animate_open && open_delay <= 0 && !animating)
        {
            Debug.Log("Door opening");
            AnimationState doorState = doorAnimation["Gateopen"];
            doorState.time = 0f;
            doorState.speed = 1;
            doorAnimation.Play();
            animating = true;
        }

        if (animate_open && !doorAnimation.isPlaying && open_delay <= 0)
        {
            Debug.Log("Door open finished");
            animate_open = false;
            animating = false;
            animate_close = true;
        }

        if (animate_close && close_delay > 0)
        {
            close_delay -= Time.deltaTime;
        }

        if (animate_close && close_delay <= 0 && !animating)
        {
            Debug.Log("Closing door!");
            AnimationState doorState = doorAnimation["Gateopen"];
            doorState.time = doorState.length;
            doorState.speed = -1;
            doorAnimation.Play();
            animating = true;
        }

        if (animate_close && animating && !doorAnimation.isPlaying)
        {
            Debug.Log("Door Close finish!");
            animate_close = false;
            animating = false;
            open_delay = od;
            close_delay = cd;
        }
	}

    public void animateDoor()
    {
        Debug.Log("Animate the door!");
        animate_open = true;
    }
}
