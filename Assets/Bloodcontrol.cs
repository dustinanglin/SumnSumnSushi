using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bloodcontrol : MonoBehaviour {

    private TimeManipulator timeMan;
    private ParticleSystem.MainModule myMain;
    private ParticleSystem myPart;
    public bool bleeding = false;
    public bool first_play = true;

	// Use this for initialization
	void Start () {
        timeMan = GameObject.Find("TimeController").GetComponent<TimeManipulator>();
        myPart = GetComponent<ParticleSystem>();
        myMain = myPart.main;
	}
	
	// Update is called once per frame
	void Update () {
	    if (bleeding)
        {
            if (first_play)
            {
                first_play = false;
                myPart.Play();
            }
            myMain.simulationSpeed = timeMan.timescale;
        }
	}
}
