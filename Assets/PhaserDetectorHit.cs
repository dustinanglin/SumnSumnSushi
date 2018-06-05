using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaserDetectorHit : MonoBehaviour {

    public bool miss = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        if (other.name.Contains("Sphere") && !miss)
        {
            GameObject.Destroy(other.gameObject.transform.parent.gameObject);
        }
    }

}
