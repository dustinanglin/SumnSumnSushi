using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterFade : MonoBehaviour {

    private Material transporter_base;
    private AudioSource transport;
    public float fade_in_time, fade_out_time = 1f;
    private bool transport_in = true;
    public bool transport_out = false;
    private float fade_time_start;

	// Use this for initialization
	void Start () {
        transporter_base = GetComponent<Renderer>().material;
        fade_time_start = fade_in_time;
        transport = GetComponent<AudioSource>();
        transport.Play();
   	}
	
	// Update is called once per frame
	void Update () {

        if (transport_in && fade_time_start > 0)
        {
            transporter_base.color = new Color(transporter_base.color.r, transporter_base.color.g, transporter_base.color.b, fade_time_start / fade_in_time);
            fade_time_start -= Time.deltaTime;
        }
        else
            transport_in = false;
        
        if (transport_out)
        {
            transporter_base.color = new Color(transporter_base.color.r, transporter_base.color.g, transporter_base.color.b, fade_time_start / fade_out_time);
            fade_time_start += Time.deltaTime;
        }
		
	}
}
