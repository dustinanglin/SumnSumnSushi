using System.Collections;
using System.Collections.Generic;
using MeshSplitting.Examples;
using UnityEngine;

public class SaberInteraction : MonoBehaviour
{
    public GameObject startSphere, endSphere, velocityTracker;

    private Vector3 startPoint, endPoint, velocity;
    private GameObject cutter;
    private Rigidbody saberRB;

    private bool doingCut = false;

    // Start is called before the first frame update
    void Start()
    {
        cutter = GameObject.Find("CenterEyeAnchor");
        saberRB = velocityTracker.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _tempVelocity = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch);
        //Debug.Log("Magnitude " + Vector3.Magnitude(_tempVelocity));
        velocity = Vector3.Normalize(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch)) * .5f;

        //Debug.Log("Velocity: " + velocity);
        Debug.DrawRay(transform.position, transform.up, Color.blue);
        if (Vector3.Magnitude(_tempVelocity) >= .02f)
            Debug.DrawRay(transform.position, velocity, Color.red);
    }

    /*void FixedUpdate()
    {
        saberRB.MovePosition(transform.position);
        saberRB.MoveRotation(transform.rotation);
        Debug.Log("Angular Velocity: " + saberRB.angularVelocity);
    }*/

    private void OnCollisionEnter(Collision collision)
    {


        collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Debug.Log("Colliding point: " + collision.GetContact(0).point);
        startSphere.transform.position = collision.GetContact(0).point;
        startPoint = collision.GetContact(0).point;
        endPoint = transform.position + velocity;
        endSphere.transform.position = endPoint;

        cutter.GetComponent<CameraLineSplitter>().doColliderCut(startPoint, endPoint, transform.up);

        if (collision.gameObject.GetComponent<CutoutMotor>())
        {
            collision.gameObject.GetComponent<CutoutMotor>().SetSpeed(0);
            collision.gameObject.GetComponent<CutoutMotor>().MotorOff();
            collision.gameObject.GetComponent<CutoutMotor>().SetDeathTimer();

        }
    }         

    private void OnCollisionStay(Collision collision)
    {
        //endPoint = collision.GetContact(collision.contactCount - 1).point;
        //endSphere.transform.position = endPoint;
        //if (collision.gameObject.GetComponent<CutoutMotor>())
            //collision.gameObject.GetComponent<CutoutMotor>().SetSpeed(2);
    }

    private void OnCollisionExit(Collision collision)
    {
        /*Debug.Log("Do Plane Cut!");
        collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        if (collision.gameObject.GetComponent<CutoutMotor>())
        {
            GameObject _obj = collision.gameObject;
            _obj.GetComponent<CutoutMotor>().MotorOff();
            _obj.GetComponent<CutoutMotor>().SetDeathTimer();
        }
        cutter.GetComponent<CameraLineSplitter>().doColliderCut(startPoint, endPoint,transform.up);*/


    }

    private void OnTriggerEnter(Collider other)
    {
       startSphere.transform.position = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
    }
}
