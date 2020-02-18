using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingBellAnimator : MonoBehaviour
{

    private AnimationState bell_press, bell_ring;
    private float distance_from_bottom, total_distance, percent_pressed;
    public float return_speed;
    private Vector3 closepoint;
    private GameObject pressing_object, ringer, hiddenDoor, bell;
    private Transform ringer_bottom, ringer_top;
    private bool pressing, pushed;
    private AudioSource bellRing;
    private AudioSource door_open;
    private Animator doorAnimator;

    // Start is called before the first frame update
    void Start()
    {
        bell_press = transform.Find("RingerNew").GetComponent<Animation>()["BellRing"];
        bell_press.speed = 0;
        //bell_press.normalizedTime = .5f;

        bellRing = GetComponentInChildren<AudioSource>();
        hiddenDoor = GameObject.Find("HiddenDoor");
        doorAnimator = hiddenDoor.GetComponent<Animator>();
        door_open = hiddenDoor.GetComponents<AudioSource>()[0];

        bell = GameObject.Find("Bell_total");
        bell_ring = bell.GetComponent<Animation>()["Ring"];

        ringer_bottom = transform.Find("RingBottom");
        ringer_top = transform.Find("RingTop");
        total_distance = ringer_top.position.y - ringer_bottom.position.y;

        pressing = false;
        pushed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (pressing)
        {
            percent_pressed = (closepoint.y - ringer_bottom.position.y) / total_distance;
            /*if (percent_pressed < 0)
                percent_pressed = 0;

            if (percent_pressed > 1)
                percent_pressed = 1;*/
            Debug.Log(percent_pressed);
            bell_press.normalizedTime = Mathf.Clamp(1 - percent_pressed, 0, 1);

            if ((1 - percent_pressed) >= 1 && !pushed)
            {
                pushed = true;
                bell_ring.time = 0;
                bell.GetComponent<Animation>().Play("Ring");
                bellRing.Play();
                Debug.Log("Pushed");
                doorAnimator.SetBool("OpenDoor", true);
                door_open.Play();
            }
            else if ((1 - percent_pressed) <= .1f && pushed)
            {
                pushed = false;
            }
        }
        else
        {
            bell_press.normalizedTime = Mathf.Lerp(bell_press.time, 0, return_speed);
            pushed = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        pressing_object = other.gameObject;
        pressing = true;
    }

    private void OnTriggerStay(Collider other)
    {
        closepoint = other.ClosestPoint(ringer_bottom.position);
    }


    private void OnTriggerExit(Collider other)
    {
        //pressing_object = null;
        pressing = false;
    }
}
