using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public float damper = 1;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		/*float currentAngle = transform.eulerAngles.y;
		float desiredAngle = player.transform.eulerAngles.y;
		float angle = Mathf.LerpAngle (currentAngle, desiredAngle, Time.deltaTime * damper);

		Quaternion rotation = Quaternion.Euler (0, angle, 0);
		transform.position = player.transform.position - (rotation * offset);*/
		transform.position = player.transform.position + offset;
		//transform.LookAt(player.transform);
	}
}
