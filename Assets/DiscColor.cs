using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscColor : MonoBehaviour {

    public bool bad_guy = false;
    public Material good_mat, bad_mat;
    public Light good_light, bad_light;
    private Renderer disc_rend;
    public GameObject disc;

  

	// Use this for initialization
	void Start () {
        disc_rend = disc.GetComponent<Renderer>();
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
	}
	
	// Update is called once per frame
	void Update () {
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
}
