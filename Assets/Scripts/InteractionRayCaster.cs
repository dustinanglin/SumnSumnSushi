using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractionRayCaster : MonoBehaviour {

	public float distanceToObject;
	public int smoother;
	public float throwPower;
	private float collisionDistance;
	private Text targetText;
	private int layerMask;
	private bool carrying;
	private GameObject carriedObject;

	void Start () {
		distanceToObject = 3;
		throwPower = 50;
		smoother = 30;
		collisionDistance = 3;
		targetText = GameObject.Find("CenterUI").GetComponent<Text>();
		layerMask = 1 << 8; //Layer 8 is Ground
		layerMask = ~layerMask; //We want the mask to be everything but 8
		carrying = false;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay (transform.position, transform.forward * distanceToObject, Color.magenta);

		if (carrying) {
			Carry (carriedObject);
			if (Input.GetMouseButtonDown(0))
				Drop(carriedObject);
		}
		else 
		{
			PickUp();
		}

			
	}

	void Carry(GameObject carObj)
	{
		Rigidbody rb = carObj.GetComponent<Rigidbody> ();
		rb.isKinematic = true;
		carObj.transform.position = Vector3.Lerp (carObj.transform.position, transform.position + transform.forward * collisionDistance, Time.deltaTime * smoother);
		Debug.Log (carObj.GetComponent<Pickupable> ().objVelocity);
	}

	void Drop (GameObject carObj)
	{
		Vector3 vel = carObj.GetComponent<Pickupable> ().objVelocity;
		Rigidbody rb = carObj.GetComponent<Rigidbody> ();
		rb.isKinematic = false;
		carrying = false;
		Debug.Log (carObj.GetComponent<Pickupable> ().objVelocity);
		rb.AddForce (vel * throwPower );
	}

	void PickUp(){

		RaycastHit hit;
		if (Physics.Raycast (transform.position, transform.forward, out hit, distanceToObject, layerMask)) {
			Pickupable p = hit.collider.GetComponent<Pickupable> ();
			if (p != null) {
				targetText.fontStyle = FontStyle.Bold;
				targetText.color = Color.red;
				targetText.fontSize = 24;
				collisionDistance = hit.distance;
				if (Input.GetMouseButtonDown (0)) {
					carriedObject = p.gameObject;
					carrying = true;
				}
			}

		}else {
			targetText.fontStyle = FontStyle.Normal;
			targetText.color = Color.black;
			targetText.fontSize = 20;
			collisionDistance = 0;
		}
	}

}
