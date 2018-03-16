using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManipulator : MonoBehaviour {

    private GameObject lefthand, righthand, head;
    private Vector3 prev_r, prev_l, prev_h;
    public float timescale = .1f;
    public float tweek = 1;
    public float base_speed = .005f;
    private bool fire_boost = false;
    public float boost_time = 1f;
    private float temp_time;
    public float boost_value = .5f;

	// Use this for initialization
	void Start () {
        //Time.timeScale = .5f;
        lefthand = GameObject.Find("LeftHandAnchor");
        righthand = GameObject.Find("RightHandAnchor");
        head = GameObject.Find("CenterEyeAnchor");

        prev_r = righthand.transform.position;
        prev_l = lefthand.transform.position;
        prev_h = head.transform.position;

        temp_time = boost_time;
    }
	
	// Update is called once per frame
	void Update () {
        float dx, dy, dz, v;
        float[] vt = new float[3];
        float current_scale;
        dx = righthand.transform.position.x - prev_r.x;
        dy = righthand.transform.position.y - prev_r.y;
        dz = righthand.transform.position.z - prev_r.z;
        vt[0] = Mathf.Clamp((Mathf.Sqrt(dx * dx + dy * dy + dz * dz) / Time.unscaledDeltaTime),0,5);

        dx = lefthand.transform.position.x - prev_l.x;
        dy = lefthand.transform.position.y - prev_l.y;
        dz = lefthand.transform.position.z - prev_l.z;
        vt[1] = Mathf.Clamp((Mathf.Sqrt(dx * dx + dy * dy + dz * dz) / Time.unscaledDeltaTime), 0, 5);

        dx = head.transform.position.x - prev_h.x;
        dy = head.transform.position.y - prev_h.y;
        dz = head.transform.position.z - prev_h.z;
        vt[2] = Mathf.Clamp((Mathf.Sqrt(dx * dx + dy * dy + dz * dz) / Time.unscaledDeltaTime), 0, 5) * .1f;



        v = Mathf.Max(vt);

        prev_r = righthand.transform.position;
        prev_l = lefthand.transform.position;
        prev_h = head.transform.position;

        if (v != 0)
        {
            if (v < .01)
                v = 0;
            timescale = v / tweek + base_speed;
        }

        if (fire_boost && temp_time >= 0)
        {
            timescale = boost_value * temp_time;
            temp_time -= Time.deltaTime;
        }
        else
            fire_boost = false;

            
        //Time.timeScale = v / 5;

	}

    public void FireBoost()
    {
        temp_time = boost_time;
        fire_boost = true;
    }
}
