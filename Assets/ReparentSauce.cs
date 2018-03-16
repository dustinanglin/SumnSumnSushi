using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReparentSauce : MonoBehaviour {

    private TrolleyAnimator trolleyAnimator;

	// Use this for initialization
	void Start () {
        trolleyAnimator = GameObject.Find("TrackHolder").GetComponent<TrolleyAnimator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerExit(Collider other)
    {
        //Debug.Log(other.gameObject.name);
        if (other.gameObject.transform.parent.gameObject.name.Contains("SauceBottle"))
        {
            Debug.Log("Removing from Trolley and restarting animation!");
            other.gameObject.transform.parent.gameObject.transform.SetParent(null);
            trolleyAnimator.restartTrolley();
        }
    }
}
