using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingButton : MonoBehaviour {

    public bool pushed;
    private bool do_vending = false;
    private float td, tp;
    public float trolley_delay, trolley_pause;
    public GameObject sauce_bottle;
    private TrolleyAnimator trolleyAnimator;
    private Animator screenAnimator;
    private Animation trolley_animation;
    private Animation left_door_animation, right_door_animation;

	// Use this for initialization
	void Start () {
        pushed = false;
        screenAnimator = GameObject.Find("VendingScreen").GetComponent<Animator>();

        trolleyAnimator = GameObject.Find("TrackHolder").GetComponent<TrolleyAnimator>();

        trolley_animation = GameObject.Find("TrolleyFinal").GetComponent<Animation>();
        left_door_animation = GameObject.Find("LeftGate").GetComponent<Animation>();
        right_door_animation = GameObject.Find("RightGate").GetComponent<Animation>();

        td = trolley_delay;
        tp = trolley_pause;
	}
	
	// Update is called once per frame
	void Update () {
		if (do_vending)
        {
            trolleyAnimator.doTrolleyAnimate();
            do_vending = false;

            //left_door_animation.Play();

            //if (td > 0f)
            //{
            //    td -= Time.deltaTime;
            //}
            //else if (tp > 0f)
            //{
            //    //Debug.Log("Animating Trolley!");
            //    trolley_animation.Play();
            //    tp -= Time.deltaTime;
            //}
            //else
            //    trolley_animation.Stop();
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.name == "Left" || other.gameObject.name == "Right") && !pushed)
        {
            Debug.Log("Button " + this.name + " Pressed!");
            pushed = true;
            screenAnimator.SetBool("DoScreenMove", false);


            GameObject sb = Instantiate(sauce_bottle);
            sb.transform.parent = GameObject.Find("SauceHolder").transform;
            sb.transform.localPosition = Vector3.zero;
            sb.GetComponent<Rigidbody>().isKinematic = true;

            td = trolley_delay;
            tp = trolley_pause;
            do_vending = true;

        }
    }


}
