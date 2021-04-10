using System.Collections;
using System.Collections.Generic;
using MeshSplitting.Examples;
using UnityEngine;

public class SaberInteraction : MonoBehaviour
{
    public GameObject startSphere, endSphere;

    private Vector3 startPoint, endPoint;
    private GameObject cutter;

    // Start is called before the first frame update
    void Start()
    {
        cutter = GameObject.Find("CenterEyeAnchor");   
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.up, Color.blue);
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        Debug.Log("Colliding point: " + collision.GetContact(0).point);
        startSphere.transform.position = collision.GetContact(0).point;
        startPoint = collision.GetContact(0).point;
    }

    private void OnCollisionStay(Collision collision)
    {
        endPoint = collision.GetContact(collision.contactCount - 1).point;
        endSphere.transform.position = endPoint;
        if (collision.gameObject.GetComponent<CutoutMotor>())
            collision.gameObject.GetComponent<CutoutMotor>().SetSpeed(2);
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Do Plane Cut!");
        collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        if (collision.gameObject.GetComponent<CutoutMotor>())
        {
            GameObject _obj = collision.gameObject;
            _obj.GetComponent<CutoutMotor>().MotorOff();
            _obj.GetComponent<CutoutMotor>().SetDeathTimer();
        }
        cutter.GetComponent<CameraLineSplitter>().doColliderCut(startPoint, endPoint,transform.up);
    }

    private void OnTriggerEnter(Collider other)
    {
       startSphere.transform.position = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
    }
}
