using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankAI : MonoBehaviour
{
    public float gun_pitch_limit, aim_speed, swivel_speed, fire_angle_threshold;
    public bool aimAtPlayer, fireAtPlayer;
    private float angleToTarget;
    private Transform AimPoint, Canon, Turret, Canon_initial, Turret_initial, Canon_Fire;
    private Quaternion initialTurretRotation, newTurretRotation, initialCanonRotation, newCanonRotation;

    // Start is called before the first frame update
    void Start()
    {
        AimPoint = GameObject.Find("Player/OVRCameraRig/TrackingSpace/CenterEyeAnchor/ShootTarget").transform;
        //AimPoint = GameObject.Find("Player/OVRCameraRig/TrackingSpace/CenterEyeAnchor").transform;

        Turret = transform.Find("Tank_Cab");
        Canon = transform.Find("Tank_Cab/Canon");
        Canon_initial = Turret.Find("Canon_Initial");
        Turret_initial = transform.Find("CabInitial");
        Canon_Fire = transform.Find("Tank_Cab/Canon/Gun_Barrel/Bullet");
    }

    // Update is called once per frame
    void Update()
    {
        if (aimAtPlayer)
            AimAtGodzilla();
        if (fireAtPlayer)
            FireAtGodzilla();
    }

    private void AimAtGodzilla()
    {
        initialTurretRotation = Turret_initial.transform.rotation;
        initialCanonRotation = Canon_initial.transform.rotation;

        Vector3 AimVector;

        //********************************
        //**** Rotate Turret Section *****
        //********************************

        AimVector = Turret.position - AimPoint.position; //Get a Vector from Turret to the Player
        AimVector = Vector3.ProjectOnPlane(AimVector, Turret.up); //Project onto a "floor" plane so it only rotates side to sdie
        newTurretRotation = Quaternion.LookRotation(AimVector, Turret.up); //Get a rotation using our projected vector

        Quaternion q_Turret = newTurretRotation; //Temporary variable in case we want to do any kind of limits to the rotation
        
        Turret.rotation = Quaternion.Slerp(Turret.rotation, q_Turret, aim_speed * Time.deltaTime); //Rotate the turret with a smooth motion
        angleToTarget = Quaternion.Angle(Turret.rotation, q_Turret); //Used to determine if the turret has finished rotating so it can fire

        //********************************
        //***** Swivel Canon Section *****
        //********************************

        AimVector = Canon.position - AimPoint.position; //Get a Vector from Turret to the Player
        AimVector = Vector3.ProjectOnPlane(AimVector, Canon_initial.right); //Project onto a "side wall" plane so it only rotates up and down
        newCanonRotation = Quaternion.LookRotation(AimVector, Canon_initial.up); //Get a rotation using our projected vector


        Quaternion q_Canon = newCanonRotation; //Temporary variable so we can determine if it's hit the rotation limit

        //Debug.Log("Euler angle: " + q_Canon.eulerAngles.x);

        //If the angle between the current canon's rotation and the initial position
        //has exceeded the angle limit, trying to aim at the player
        if (Quaternion.Angle(initialCanonRotation, Canon.rotation) >= gun_pitch_limit)
            q_Canon = Canon.rotation;

        //If the angle between the current canon's rotation and the new spot it is aiming toward
        //is less than the angle limit, go back to rotating
        if (Quaternion.Angle(initialCanonRotation, newCanonRotation) < gun_pitch_limit)
            q_Canon = newCanonRotation;

        Canon.rotation = Quaternion.Slerp(Canon.rotation, q_Canon, swivel_speed * Time.deltaTime); //Rotate the canon with a smooth motion
    }

    private void FireAtGodzilla()
    {
        //Debug.Log("Angle to Target: " + Mathf.FloorToInt(angleToTarget));

        if (Mathf.FloorToInt(angleToTarget) == 0)
        {
            if (!Canon_Fire.gameObject.GetComponent<ParticleSystem>().isPlaying)
                Canon_Fire.gameObject.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            Canon_Fire.gameObject.GetComponent<ParticleSystem>().Stop();
        }
    }


    // ******** LEGACY CODE FOR FUTURE ANTHROPOLOGISTS TO DISCOVER *********


    //private void AimAtGodzilla()
    //{
    //    float pitch, yaw;

    //    Vector3 toAimPointGun = AimPoint.position - Canon.position;
    //    pitch = Mathf.Clamp(Vector3.SignedAngle(-Canon_initial.forward, Vector3.ProjectOnPlane(toAimPointGun, Canon_initial.right), Canon_initial.right), -gun_pitch_limit, gun_pitch_limit);
    //    //Debug.Log(Vector3.SignedAngle(-Canon_initial.forward, Vector3.ProjectOnPlane(toAimPointGun, Canon_initial.right), Canon_initial.right));


    //    Canon.rotation = Quaternion.Slerp(Canon.rotation, Canon_initial.rotation * Quaternion.AngleAxis(pitch, Vector3.right), aim_speed);

    //    Vector3 toAimPointCab = AimPoint.position - Cab.position;
    //    yaw = Vector3.SignedAngle(-Cab_initial.forward, Vector3.ProjectOnPlane(toAimPointCab, Cab_initial.up), Cab_initial.up);
    //    //Debug.Log("Cab Yaw:" + yaw);
    //    Cab.rotation = Quaternion.Slerp(Cab.rotation, Cab_initial.rotation * Quaternion.AngleAxis(yaw, Vector3.up), swivel_speed);

    //}

}
