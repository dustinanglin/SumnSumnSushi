using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterFade : MonoBehaviour {

    private Material transporter_base;
    public float fade_time = 1f;
    private float fade_time_start;

	// Use this for initialization
	void Start () {
        transporter_base = GetComponent<Renderer>().material;
        fade_time_start = fade_time;
	}
	
	// Update is called once per frame
	void Update () {

        transporter_base.color = new Color(transporter_base.color.r, transporter_base.color.g, transporter_base.color.b, fade_time_start / fade_time);
        fade_time_start -= Time.deltaTime;
		
	}
}
