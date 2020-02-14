using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour {

    public bool isGrabbed = false;
    private Rigidbody object_rigid_body;
    private GameObject grabbing_parent;
    private ChopstickRotateOculus parent_info;
    private List<GameObject> children = new List<GameObject>();
    Vector3 grab_offset_pos;
    Vector3 linear_velocity;
    Vector3 angular_velocity;
    Vector3 prev_linear_velocity;
    Vector3 prev_angular_velocity;

    Quaternion grab_offset_rot;

	// Use this for initialization
	void Start () {
        object_rigid_body = this.GetComponent<Rigidbody>();
        if (transform.childCount > 0)
        {
            foreach (Transform child in transform)
            {
                children.Add(child.gameObject);
            }
        }
        //Debug.Log("Got children");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (isGrabbed)
        {
            GrabMove();
        }
	}

    public void GrabBegin(GameObject parent_anchor, GameObject parent_sticks)
    {
        isGrabbed = true;
        grabbing_parent = parent_anchor;
        parent_info = parent_sticks.GetComponent<ChopstickRotateOculus>();
        object_rigid_body.isKinematic = true;
        //object_rigid_body.interpolation

        
        this.gameObject.layer = 20;
        
        

        //set relative position offset
        Vector3 relPos = this.transform.position - grabbing_parent.transform.position;
        relPos = Quaternion.Inverse(grabbing_parent.transform.rotation) * relPos;
        grab_offset_pos = relPos;

        //set relative rotation offset
        Quaternion relRot = Quaternion.Inverse(grabbing_parent.transform.rotation) * this.transform.rotation;
        grab_offset_rot = relRot;

        if (transform.childCount > 0)
        foreach (GameObject child in children)
        {
            child.layer = 20;
        }

        if (transform.gameObject.name.Contains("Combadge"))
        {
            GameObject.Find("SceneDirector").GetComponent<SceneDirector>().SetTrek();
            AudioSource chirp = transform.gameObject.GetComponent<AudioSource>();
            if (!chirp.isPlaying)
            {
                chirp.Play();
            }
        }

    }

    public void GrabEnd()
    {
        if (this)
        {
            isGrabbed = false;
            this.gameObject.layer = 0;
            if (transform.childCount > 0)
                foreach (GameObject child in children)
                {
                    child.layer = 0;
                }

            /*OVRPose localPose = new OVRPose { position = OVRInput.GetLocalControllerPosition(parent_info.current_controller), orientation = OVRInput.GetLocalControllerRotation(parent_info.current_controller) };
            OVRPose offsetPose = new OVRPose { position = grab_offset_pos, orientation = grab_offset_rot};
            localPose = localPose * offsetPose;

            OVRPose trackingSpace = grabbing_parent.transform.ToOVRPose() * localPose.Inverse();
            Vector3 linearVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerVelocity(parent_info.current_controller);
            Vector3 angularVelocity = trackingSpace.orientation * OVRInput.GetLocalControllerAngularVelocity(parent_info.current_controller);

           */
            /*Debug.Log("At time of release:");
            Debug.Log(linear_velocity);
            Debug.Log(angular_velocity);*/

            object_rigid_body.isKinematic = false;
            object_rigid_body.velocity = prev_linear_velocity * 1.5f;
            object_rigid_body.angularVelocity = prev_angular_velocity;
            grabbing_parent = null;
        }
    }

    void GrabMove()
    {
        Vector3 newPos = grabbing_parent.transform.position + grabbing_parent.transform.rotation * grab_offset_pos;
        Quaternion newRot = grabbing_parent.transform.rotation * grab_offset_rot;
        object_rigid_body.MovePosition(newPos);
        object_rigid_body.MoveRotation(newRot);

        prev_linear_velocity = linear_velocity;
        prev_angular_velocity = angular_velocity;

        linear_velocity = object_rigid_body.velocity;
        angular_velocity = object_rigid_body.angularVelocity;

        /*Debug.Log("Last recorded:");
        Debug.Log(linear_velocity);
        Debug.Log(angular_velocity);*/
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Destroyer")
        {
            Destroy(gameObject);
           // Debug.Log("Fish destroyed!");
        }
    }
}
