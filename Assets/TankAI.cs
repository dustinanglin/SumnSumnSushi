using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : MonoBehaviour
{
    public float gun_pitch_limit, aim_speed, swivel_speed;
    private Transform AimPoint, Canon, Cab, Canon_initial, Cab_initial;
    // Start is called before the first frame update
    void Start()
    {
        AimPoint = GameObject.Find("Player/OVRCameraRig/TrackingSpace/CenterEyeAnchor/ShootTarget").transform;

        Cab = transform.Find("Tank_Cab");
        Canon = transform.Find("Tank_Cab/Gun_Swivel");
        Canon_initial = Cab.Find("SwivelInitial");
        Cab_initial = transform.Find("CabInitial");
    }

    // Update is called once per frame
    void Update()
    {
        AimAtGodzilla();
    }

    private void AimAtGodzilla()
    {
        float pitch, yaw;

        Vector3 toAimPointGun = AimPoint.position - Canon.position;
        pitch = Mathf.Clamp(Vector3.SignedAngle(-Canon_initial.forward, Vector3.ProjectOnPlane(toAimPointGun, Canon_initial.right), Canon_initial.right), -gun_pitch_limit,gun_pitch_limit);
        //Debug.Log(Vector3.SignedAngle(-Canon_initial.forward, Vector3.ProjectOnPlane(toAimPointGun, Canon_initial.right), Canon_initial.right));
 

        Canon.rotation = Quaternion.Slerp(Canon.rotation,Canon_initial.rotation * Quaternion.AngleAxis(pitch, Vector3.right),aim_speed);

        Vector3 toAimPointCab = AimPoint.position - Cab.position;
        yaw = Vector3.SignedAngle(-Cab_initial.forward, Vector3.ProjectOnPlane(toAimPointCab, Cab_initial.up), Cab_initial.up);
        //Debug.Log("Cab Yaw:" + yaw);
        Cab.rotation = Quaternion.Slerp(Cab.rotation,Cab_initial.rotation * Quaternion.AngleAxis(yaw, Vector3.up),swivel_speed);


    }
}
