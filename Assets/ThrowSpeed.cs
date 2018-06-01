using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowSpeed : MonoBehaviour {
    public Vector3 m_velocity = new Vector3(0, 0, 0);
    private bool is_right;

	// Use this for initialization
	void Start () {
        is_right = this.name.Contains("Right");
	}

    // Update is called once per frame
    void Update() {
        //Debug.Log(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch));
        if (is_right)
        {
            Debug.DrawRay(transform.position, OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch), Color.green);
            if (Vector3.Magnitude(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch)) > 0)
                m_velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
        }
        else
        {
            Debug.DrawRay(transform.position, OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch), Color.green);
            if (Vector3.Magnitude(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch)) > 0)
                m_velocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch);
        }
   
            }
}
