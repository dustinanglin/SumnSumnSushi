using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveOscillator : MonoBehaviour {
    public float rotate_speed = 1;
    public float x_amp = 1;
    public float y_amp = 1;
    public float offset;
    private float timedelta = 0;
    private TimeManipulator timeMan;
    private Vector3 init_position;
  
	// Use this for initialization
	void Start () {
        if (GameObject.Find("TimeController"))
            timeMan = GameObject.Find("TimeController").GetComponent<TimeManipulator>();

        init_position = transform.position;
        
        //timedelta += offset;
	}
	
	// Update is called once per frame
	void Update () {
        float x, y;
        if (timeMan)
            timedelta += (Time.deltaTime * timeMan.timescale);
        else
            timedelta += Time.deltaTime;
        x = Mathf.Sin(rotate_speed * timedelta + offset);
        y = Mathf.Cos(rotate_speed * timedelta + offset);

        //Debug.Log("X:" + x +" Y:" +y);


        this.transform.position = new Vector3(x_amp * x + init_position.x, y_amp * y + init_position.y, init_position.z);
	}
}
