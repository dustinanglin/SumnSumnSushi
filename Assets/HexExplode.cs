using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexExplode : MonoBehaviour {

    private Animation myClip;
    public float play_speed = 2.5f;

	// Use this for initialization
	void Start () {
        myClip = this.GetComponent<Animation>();
        myClip["Explode"].wrapMode = WrapMode.Once;
    }
	
	// Update is called once per frame
	void Update () {
        myClip["Explode"].speed = play_speed;
    }
}
