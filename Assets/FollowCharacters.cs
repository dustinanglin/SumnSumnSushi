using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCharacters : MonoBehaviour {

    private GameObject player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("MouthCollider");
	}
	
	// Update is called once per frame
	void Update () {
        if(this.GetComponent<ParticleSystem>().isStopped)
            transform.position = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
	}
}
