using UnityEngine;
using System.Collections;

public class Pickupable : MonoBehaviour {

	public Vector3 objVelocity;
	private Vector3 prevPos;
	private Vector3 currPos;

	// Use this for initialization
	void Start () {
		prevPos = transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		currPos = transform.position;
		objVelocity = (currPos - prevPos) / Time.deltaTime;
		prevPos = currPos;
	}
}
