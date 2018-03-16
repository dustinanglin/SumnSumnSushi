using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightonoff : MonoBehaviour {

	public Material lighton;
	public Material lightoff;
	public bool isLightOn = true;

	private Renderer rend;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		rend.enabled = true;
        setLight();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Submit")) {
			if (isLightOn) {
				isLightOn = false;
			} else {
				isLightOn = true;
			}
            setLight();
		}
			
	}

    private void setLight()
    {
        if (isLightOn)
            rend.material = lighton;
        else
            rend.material = lightoff;
    }
}
