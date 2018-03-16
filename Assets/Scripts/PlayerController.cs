using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	private Rigidbody rb;
	public float speed = 10;
	public float jumpSpeed = 10;

	void Start(){
		rb = GetComponent<Rigidbody> ();
	}
	
	void FixedUpdate () {
		/*float moveVertical = Input.GetAxis ("Vertical");
		float moveHorizontal = Input.GetAxis ("Horizontal");

		Vector3 movement = new Vector3 (moveHorizontal, 0, moveVertical);

		rb.AddForce (movement * speed);

		if (Input.GetButtonDown ("Jump") && transform.position.y <= 0.5 ) {
			rb.AddForce (0, jumpSpeed, 0);*/
		}
	}
	
