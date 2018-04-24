using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSpeed : MonoBehaviour {
    public Vector3 m_velocity = new Vector3(0, 0, 0);
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch));
        Debug.DrawRay(transform.position, OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch), Color.green);
        if (Vector3.Magnitude(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch)) > 0)
            m_velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
            }
}
