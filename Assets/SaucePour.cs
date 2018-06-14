using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaucePour : MonoBehaviour {

    private ParticleSystem sauce;
    private ParticleSystem.EmissionModule sauce_emit;
    private ParticleSystem.MainModule sauce_main;
    private float pour_angle, flow_amount;
    public bool pour_sauce;
    public float min_pour_angle = 60f;
    public float sauce_size = .3f;
    public float sauce_size_min = .1f;
    public float max_float_rate = 500f;

	// Use this for initialization
	void Start () {
        sauce = this.GetComponentInChildren<ParticleSystem>();
        sauce_emit = sauce.emission;
        sauce_main = sauce.main;
        pour_sauce = false;
	}
	
	// Update is called once per frame
	void Update () {
        pour_angle = Vector3.Angle(transform.forward, Vector3.down);

        if (pour_angle < min_pour_angle)
        {

            flow_amount = (min_pour_angle - pour_angle)/min_pour_angle;
            sauce_emit.rateOverTime = flow_amount * max_float_rate;
            //sauce_main.startSize = Mathf.Clamp(sauce_size * flow_amount,sauce_size_min,sauce_size);
            sauce_emit.enabled = true;

        }
        else
            sauce_emit.enabled = false;
	}

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(this.transform.position, this.transform.forward);
    }
}
