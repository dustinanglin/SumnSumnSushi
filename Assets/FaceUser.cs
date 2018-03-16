using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceUser : MonoBehaviour {

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("UserTarget");
	}
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(player.transform, transform.up);
	}
}
