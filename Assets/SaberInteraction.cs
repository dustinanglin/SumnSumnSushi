using System.Collections;
using System.Collections.Generic;
using MeshSplitting.Examples;
using MeshSplitting.Splitables;
using UnityEngine;

public class SaberInteraction : MonoBehaviour
{
    public GameObject startSphere, endSphere, velocityTracker;

    [SerializeField]
    private float bladeOffset1, bladeOffset2, bladeCapOffset;

    [SerializeField]
    private float bladeGlowSpeed;

    [SerializeField]
    private float bladeGlowVariance;

    [SerializeField]
    private float bladeWidth;

    [SerializeField]
    private float bladeDropOff;

    private GameObject sparks, sparksBoom;

    private Vector3 startPoint, endPoint, velocity;
    private GameObject cutter, sparkCaster;
    private Rigidbody saberRB;
    private LineRenderer bladeGlow, bladeCap;

    private bool doingCut = false;

    // Start is called before the first frame update
    void Start()
    {
        cutter = GameObject.Find("CenterEyeAnchor");
        saberRB = velocityTracker.GetComponent<Rigidbody>();
        bladeGlow = GetComponent<LineRenderer>();
        bladeCap = transform.Find("BladeCap").gameObject.GetComponent<LineRenderer>();
        sparks = transform.Find("SparkFount").gameObject;
        sparksBoom = transform.Find("SparkFountBoom").gameObject;
        sparkCaster = transform.Find("SparkCaster").gameObject;
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

        DrawBlade();
    }


    private void DrawBlade()
    {
        bladeGlow.SetPosition(2, transform.position + transform.rotation * new Vector3(0, bladeOffset1, 0));
        bladeGlow.SetPosition(1, transform.position + transform.rotation * new Vector3(0, -bladeOffset2 + bladeDropOff, 0));
        bladeGlow.SetPosition(0, transform.position + transform.rotation * new Vector3(0, -bladeOffset2 , 0));

        bladeCap.SetPosition(0, transform.position + transform.rotation * new Vector3(0, bladeOffset1, 0));
        bladeCap.SetPosition(1, transform.position + transform.rotation * new Vector3(0, bladeOffset1 + bladeCapOffset, 0));

        bladeGlow.startWidth = bladeWidth + Mathf.Sin(Time.time * bladeGlowSpeed) * bladeGlowVariance;
        bladeGlow.endWidth = bladeWidth + Mathf.Sin(Time.time * bladeGlowSpeed) * bladeGlowVariance;
        bladeCap.startWidth = bladeWidth + Mathf.Sin(Time.time * bladeGlowSpeed) * bladeGlowVariance;
        bladeCap.endWidth = bladeWidth + Mathf.Sin(Time.time * bladeGlowSpeed) * bladeGlowVariance;
    }

    /*void FixedUpdate()
    {
        saberRB.MovePosition(transform.position);
        saberRB.MoveRotation(transform.rotation);
        Debug.Log("Angular Velocity: " + saberRB.angularVelocity);
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collide!");

        startSphere.transform.position = collision.GetContact(0).point;
        startPoint = collision.GetContact(0).point;
        endPoint = transform.position + velocity;
        endSphere.transform.position = endPoint;

        if (collision.gameObject.GetComponent<Splitable>())
        {
            collision.gameObject.GetComponent<Rigidbody>().isKinematic = false;
            Debug.Log("Colliding point: " + collision.GetContact(0).point);
            cutter.GetComponent<CameraLineSplitter>().doColliderCut(startPoint, endPoint, transform.up);

            if (collision.gameObject.GetComponent<CutoutMotor>())
            {
                collision.gameObject.GetComponent<CutoutMotor>().SetSpeed(0);
                collision.gameObject.GetComponent<CutoutMotor>().MotorOff();
                collision.gameObject.GetComponent<CutoutMotor>().SetDeathTimer();
            }

            setSparkSpot(sparksBoom, collision.GetContact(0));
            sparksBoom.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            setSparkSpot(sparks);
            sparks.SetActive(true);
        }

    }
    
    private void setSparkSpot(GameObject sp)
    {
        RaycastHit hit;
        Ray ray = new Ray(sparkCaster.transform.position, transform.up);
        Physics.Raycast(sparkCaster.transform.position, transform.up,out hit,1.0f);
        sp.transform.position = hit.point;
        endSphere.transform.position = hit.point;
        sp.transform.rotation = Quaternion.LookRotation(hit.normal);
    }

    private void setSparkSpot(GameObject sp, ContactPoint point)
    {
        sp.transform.position = point.point;
        sp.transform.rotation = Quaternion.LookRotation(point.normal);
    }

    private void OnCollisionStay(Collision collision)
    {
        //endPoint = collision.GetContact(0).point;
        //endSphere.transform.position = endPoint;
        //if (collision.gameObject.GetComponent<CutoutMotor>())
        //collision.gameObject.GetComponent<CutoutMotor>().SetSpeed(2);
        setSparkSpot(sparks);
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

        sparks.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
       startSphere.transform.position = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
    }
}
