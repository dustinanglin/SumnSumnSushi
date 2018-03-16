using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightonoff_spot : MonoBehaviour {

	private Light spotLight;
	public bool isLightOn = true;
	// Use this for initialization
	void Start () {
		spotLight = GetComponent<Light> ();
        setLight();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Submit")) {
			if (isLightOn) {
				isLightOn = false;
                setLight();
			} else {
				isLightOn = true;
                setLight();
			}
		}
	}

    private void setLight()
    {
        if (isLightOn)
            spotLight.enabled = true;
        else
            spotLight.enabled = false;
    }
}
