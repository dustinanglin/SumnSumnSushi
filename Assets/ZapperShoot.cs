using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZapperShoot : MonoBehaviour {

    private GameObject trigger, zapperCaster;
    private AudioSource zapper_sound;
    private DuckHunt director;
    private OVRInput.Controller current_controller;
    private DuckFly duck_collider, duck;
    public float trigger_distance;
    private float initial_trigger_distance;
    private bool shoot, reset;
    private float trigger_down;


	// Use this for initialization
	void Start () {
        director = GameObject.Find("DuckHuntDirector").GetComponent<DuckHunt>();

        trigger = transform.Find("Trigger").gameObject;
        zapperCaster = transform.Find("ZapperCaster").gameObject;

        initial_trigger_distance = trigger.transform.localPosition.z;

        shoot = false;
        reset = true;

        current_controller = this.name.Contains("Left") ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch;
        zapper_sound = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        PullTrigger();

        if (shoot & !reset && director.ShotsRemaining() > 0)
            Shoot();

        Debug.DrawRay(zapperCaster.transform.position, zapperCaster.transform.forward * -1, Color.cyan);
    }

    public void InitiateDuck(DuckFly _duck)
    {
        duck = _duck;
    }

    private void PullTrigger()
    {
        trigger_down = OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, current_controller);

        if (trigger_down < .9f)
        {
            MoveTrigger(trigger_down);
            shoot = false;
            reset = false;
        }
        else
        {
            MoveTrigger(1);
            shoot = true;
        }

    }

    private void MoveTrigger(float percent)
    {
        trigger.transform.localPosition = new Vector3(trigger.transform.localPosition.x, trigger.transform.localPosition.y, initial_trigger_distance + trigger_distance * percent);
    }

    private void Shoot()
    {
        reset = true;
        Debug.Log("Shoot!");
        zapper_sound.Play();

        RaycastHit hit;
        Physics.Raycast(zapperCaster.transform.position, -1 * zapperCaster.transform.forward, out hit);

        if (hit.collider)
        {
            GameObject duck_maybe = hit.collider.gameObject;
            Debug.Log(duck_maybe.name);

            if (duck_maybe.name.Contains("Duck"))
            {
                duck_collider = duck_maybe.GetComponent<DuckFly>();
                duck_collider.shot = true;
            }
        }

        duck.ScareDuck();
        director.DecrementShots();
    }
}
