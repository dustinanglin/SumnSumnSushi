using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrolleyAnimator : MonoBehaviour {
    private Animation trolley;
    private DoorAnimator door1, door2;

    private bool animate_trolley1, animate_trolley2, trolley_animating, dofadeIn, dofadeOut;
    private float trolley_pause, trolley_delay;

    public float tp, td;

    public float fade_time = 1.0f;
    private float fade_local;

	// Use this for initialization
	void Start () {
        trolley = GetComponentInChildren<Animation>();

        door1 = GameObject.Find("LeftGate").GetComponent<DoorAnimator>();
        door2 = GameObject.Find("RightGate").GetComponent<DoorAnimator>();

        animate_trolley1 = false;
        animate_trolley2 = false;
        trolley_animating = false;
        dofadeIn = false;
        dofadeOut = false;

        trolley_pause = tp;
        trolley_delay = td;

        fade_local = fade_time;
        
	}
	
	// Update is called once per frame
	void Update () {

        if (animate_trolley1 && trolley_delay > 0)
        {
            trolley_delay -= Time.deltaTime;
            trolley.Play();
            trolley["TrolleyMove"].speed = 0;
        }

		if (animate_trolley1 && trolley_delay <= 0 && !trolley_animating)
        {
            trolley["TrolleyMove"].speed = 1;
            trolley_animating = true;
        }

        if (animate_trolley1 && trolley_animating && trolley_pause > 0)
        {
            trolley_pause -= Time.deltaTime;
        }

        if (animate_trolley1 && trolley_animating && trolley_pause <= 0 && !animate_trolley2)
        {
            Debug.Log("Pause the Trolley!");
            trolley.Stop();
            dofadeOut = true;
            animate_trolley1 = false;
            trolley_animating = false;
        }

        if ((animate_trolley2 && !trolley_animating) || (animate_trolley2 && animate_trolley1))
        {

            Debug.Log("Trolley is restarted!");
            trolley.Play();
            door2.animateDoor();
            trolley["TrolleyMove"].time = 10f;
            trolley_animating = true;
            animate_trolley1 = false;
        }

        if (animate_trolley2 && !trolley.isPlaying)
        {

            Debug.Log("Trolley animation is finished!");
            dofadeOut = true;
            trolley_animating = false;
            animate_trolley2 = false;
            animate_trolley1 = false;
            trolley_pause = tp;
            trolley_delay = td;
        }

        if (dofadeOut && !dofadeIn)
        {
            trolleyAudioFadeOut();
            //Debug.Log("doing fade out");
        }

        if (dofadeIn)
        {
            if (dofadeOut)
            {
                dofadeOut = false;
                fade_local = fade_time - fade_local;
            }
            trolleyAudioFadeIn();
            //Debug.Log("doing fade in");
        }

	}

    private void trolleyAudioFadeOut()
    {
        fade_local -= Time.deltaTime;
        if (fade_local > 0)
        {
            GetComponentInChildren<AudioSource>().volume = (fade_local / fade_time);
            //Debug.Log(GetComponentInChildren<AudioSource>().volume);
        }
        else
        {
            fade_local = fade_time;
            GetComponentInChildren<AudioSource>().Pause();
            dofadeOut = false;
        }
    }

    private void trolleyAudioFadeIn()
    {
        fade_local -= Time.deltaTime;
        if (fade_local > 0 && GetComponentInChildren<AudioSource>().volume != 1.0f)
        {
            GetComponentInChildren<AudioSource>().volume = 1.0f - (fade_local / fade_time);
            //Debug.Log(GetComponentInChildren<AudioSource>().volume);
        }
        else
        {
            fade_local = fade_time;
            dofadeIn = false;
        }
    }

    public void doTrolleyAnimate()
    {
        if (!trolley.isPlaying)
        {
            animate_trolley1 = true;
            door1.animateDoor();
            Debug.Log("Animate the trolley!");
            GetComponentInChildren<AudioSource>().Play();
            dofadeIn = true;
        }
    }

    public void restartTrolley()
    {
        animate_trolley2 = true;
        Debug.Log("Restart the trolley!");
        GetComponentInChildren<AudioSource>().Play();
        dofadeIn = true;
    }


}
