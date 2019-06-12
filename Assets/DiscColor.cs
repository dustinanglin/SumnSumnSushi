using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscColor : MonoBehaviour {

    public bool bad_guy = false;
    private bool standby = true;
    private bool initiated = false;
    public float blink_speed = 0f;
    public Material good_mat, bad_mat, standby_mat, off_mat;
    public Light good_light, bad_light, standby_light;
    private Renderer disc_rend;
    public GameObject disc;
    private float standby_time = 0f;


  

	// Use this for initialization
	void Start () {
        disc_rend = disc.GetComponent<Renderer>();
        good_light.enabled = false;
        bad_light.enabled = false;

	}
	
	// Update is called once per frame
	void Update () {

        if (!standby && !initiated)
        {
            standby_light.enabled = false;
            if (bad_guy)
            {
                disc_rend.material = bad_mat;
                bad_light.enabled = true;
                good_light.enabled = false;
            }
            else
            {
                disc_rend.material = good_mat;
                bad_light.enabled = false;
                good_light.enabled = true;
            }
            initiated = true;
        }

        if (standby)
        {
            standby_time += Time.deltaTime * blink_speed;
            if (Mathf.Sin(standby_time) > 0)
            {
                disc_rend.material = standby_mat;
                standby_light.enabled = true;
            }
            else
            {
                disc_rend.material = off_mat;
                standby_light.enabled = false;
            }
        }


		if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            if (bad_guy)
            {
                bad_guy = false;
                disc_rend.material = good_mat;
                bad_light.enabled = false;
                good_light.enabled = true;
            }
            else
            {
                bad_guy = true;
                disc_rend.material = bad_mat;
                bad_light.enabled = true;
                good_light.enabled = false;
            }
        }
	}

    public void SetInitiated()
    {
        standby = false;
    }
}
