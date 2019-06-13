using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundWarp : MonoBehaviour {

    private TimeManipulator timeMan;
    private AudioSource sound;
    public float min_speed, max_speed,change_rate;

	// Use this for initialization
	void Start () {
        timeMan = GameObject.Find("TimeController").GetComponent<TimeManipulator>();
        sound = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        sound.pitch = Mathf.Lerp(sound.pitch, Mathf.Clamp(timeMan.timescale,min_speed,max_speed),change_rate);
	}
}
