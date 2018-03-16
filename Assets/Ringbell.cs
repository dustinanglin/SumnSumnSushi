using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ringbell : MonoBehaviour {

    public float max_distance = .2f;
    private float start_y, start_x, start_z;
    public float spring_speed = 1;
    private bool pressing = false;
    private bool pushed = false;
    private bool playing = false;

    private float y_pos;

    private Quaternion local_rot;

    private GameObject bell;

    private AnimationState myClip;

    public AudioClip bellRing;
    private AudioSource source;

    private GameObject hiddenDoor;
    private Animator doorAnimator;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        hiddenDoor = GameObject.Find("HiddenDoor");
        doorAnimator = hiddenDoor.GetComponent<Animator>();
    }


    // Use this for initialization
    void Start () {
        start_y = transform.localPosition.y;
        start_z = transform.localPosition.z;
        start_x = transform.localPosition.x;
        local_rot = transform.localRotation;

        y_pos = start_y;

        bell = GameObject.Find("Bell_total");

        myClip = bell.GetComponent<Animation>()["Ring"];
        
        //myClip.speed = 0;
        //Debug.Log(bell.GetComponent<Animation>().IsPlaying("Ring"));

    }
	
	// Update is called once per frame
	void Update () {

        //Debug.Log(myClip.time);

        if (!pressing)
        {
            transform.localPosition += new Vector3(0, Time.deltaTime * spring_speed, 0);
            //Debug.Log("Moving back");

        }
        transform.localPosition = new Vector3(start_x, Mathf.Clamp(transform.localPosition.y, -1 * max_distance + start_y, start_y), start_z);

        float percent_pushed = Mathf.Abs(start_y - transform.localPosition.y) / max_distance;
        //Debug.Log(percent_pushed);
        transform.localRotation = local_rot;

        if (percent_pushed >= .9f && !pushed)
        {
            pushed = true;

            myClip.time = 0;
            bell.GetComponent<Animation>().Play("Ring");
            source.PlayOneShot(bellRing);
            Debug.Log("Pushed");
            doorAnimator.SetBool("OpenDoor", true);
        }
         else if (percent_pushed <= .1f && pushed)
        {
            pushed = false;
            Debug.Log("Unpushed");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        pressing = true;
        Debug.Log("Pressed");
    }

    private void OnCollisionExit(Collision collision)
    {
        pressing = false;
        Debug.Log("Unpressed");
    }
}
