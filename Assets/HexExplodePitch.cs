using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexExplodePitch : MonoBehaviour {

    private AudioSource explode;

	// Use this for initialization
	void Start () {
        explode = GetComponent<AudioSource>();
        explode.pitch = 1 + Random.Range(0, .2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
