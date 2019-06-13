using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWorldPosition : MonoBehaviour {

    public GameObject player;
    public float z_offset, x_offset = 0f;

	// Use this for initialization
	void Start () {
        this.transform.position = new Vector3(player.transform.position.x + x_offset, this.transform.position.y, player.transform.position.z + z_offset);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
