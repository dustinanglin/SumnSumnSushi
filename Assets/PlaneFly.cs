using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneFly : MonoBehaviour
{
    public float speed, pitch_speed, roll_speed, turn_time, pitch, roll, lerp_speed, angle_modifier;
    public Vector2 range;
    private float turn_time_local, turn_amount, l_p, l_r, flip;
    private bool doTurn, turn_set, pitchDone, rollDone, fired;
    private Vector3 right,forward;
    private Quaternion initial_rot, begin_pitch, begin_roll, begin_rot;
    private ParticleSystem bullets;
    private Transform AimPoint;
    private Grabbable Grabbable;
    private Rigidbody Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        turn_time_local = turn_time;

        turn_set = false;
        pitchDone = rollDone = fired = false;
        turn_amount = 0;
        l_p = 0;
        l_r = 0;
        flip = 1;


        bullets = transform.Find("Nosecone").GetComponentInChildren<ParticleSystem>();
        Grabbable = GetComponent<Grabbable>();
        Rigidbody = GetComponent<Rigidbody>();
        bullets.Clear();

        AimPoint = GameObject.Find("Player/OVRCameraRig/TrackingSpace/CenterEyeAnchor/ShootTarget").transform;
        //AimPoint = GameObject.Find("ShootTarget").transform;
        transform.rotation = Quaternion.LookRotation(transform.position - AimPoint.position,transform.up);


        begin_rot = transform.rotation;
        initial_rot = transform.rotation;
        right = transform.right;
        forward = transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, AimPoint.position, Color.red);
        if (!Grabbable.isGrabbed && Rigidbody.isKinematic)
        {
            FlyForward();
            DoTurnLogic();
            DoFireLogic();
        }
   }

    private void FlyForward()
    {
        transform.Translate(-transform.forward * speed * Time.deltaTime,Space.World);
        Debug.DrawRay(transform.position, -transform.forward);
    }

    private void DoFireLogic()
    {
        if (Vector3.Distance(transform.position, AimPoint.position) <= 2 && !fired)
        {
            bullets.Clear();
            bullets.Play();
            fired = true;
        }

        if (Vector3.Distance(transform.position, AimPoint.position) > 2)
        {
            fired = false;
        }
    }

    private void DoTurnLogic ()
    {
        if (doTurn)
        {
            if (flip > 0) //flying toward monster
            {
                if (!pitchDone)
                    DoPitch(pitch);
                else if (!rollDone)
                    DoRoll(roll);
                else
                {
                    doTurn = false;
                    turn_set = false;
                    pitchDone = false;
                    rollDone = false;
                    //turn_time_local = turn_time/2;
                    flip *= -1;
                }
            }
            else //flying away
            {
                if (!rollDone)
                    DoRoll(roll);
                else if (!pitchDone)
                    DoPitch(pitch);
                else
                {
                    doTurn = false;
                    turn_set = false;
                    pitchDone = false;
                    rollDone = false;
                    //turn_time_local = turn_time;
                    flip *= -1;

                    //do a quick correction
                    l_p = 0;
                    l_r = 0;
                    //transform.rotation = Quaternion.LookRotation(transform.position - AimPoint.position, transform.up);
                    begin_rot = transform.rotation;

                    //Debug.Log("Do Look At");
                }
            }
        }
        else if (flip > 0) //if not turning and flying forward, slowly adjust forward to aimspot
        {
            l_p = 0;
            //l_r = 0;
            var turn = Quaternion.LookRotation(transform.position - AimPoint.position, Vector3.up);
            begin_rot = Quaternion.RotateTowards(begin_rot, turn, Time.deltaTime * 20);


            var angle = Vector3.SignedAngle(Vector3.ProjectOnPlane(transform.position - AimPoint.position, Vector3.up), Vector3.ProjectOnPlane(transform.forward, Vector3.up), Vector3.up);
            float vel = 0f;
            l_r = Mathf.SmoothStep(l_r, -angle * angle_modifier, Time.deltaTime * lerp_speed);

            if (l_r < .6f && l_r > -.6f)
            {
                l_r = 0;
            }
            //l_r = Mathf.Floor(l_r);
                //Mathf.MoveTowards(l_r, -angle * angle_modifier, lerp_speed);
           // Debug.Log("Angle:" + angle);
           // Debug.Log("l_r:" + l_r);

            ////l_r = -angle * 3f; //Mathf.Lerp(l_r,-angle,Time.deltaTime);
            ////Debug.Log();
            //Debug.Log("l_r:" + l_r);
        }

        if ((Vector3.Distance(transform.position, AimPoint.position) >= range.y && flip < 0) || (Vector3.Distance(transform.position, AimPoint.position) <= range.x && flip > 0))
        {
            doTurn = true;
            if (!turn_set)
            {
                begin_rot = transform.rotation;
                l_p = 0;
                l_r = 0;
                turn_set = true;
            }
        }

        transform.rotation = Quaternion.Euler(flip * Mathf.Clamp(l_p, 0, pitch) + begin_rot.eulerAngles.x, begin_rot.eulerAngles.y, flip * Mathf.Clamp(l_r, -roll, roll) + begin_rot.eulerAngles.z);
    }

    private void DoPitch(float t_pitch)
    {
        //Debug.Log("Do Pitch");
        //Debug.Log(l_p);
        if (l_p < t_pitch)
        {
            l_p += Time.deltaTime * pitch_speed;
        }
        else
            pitchDone = true;
    }

    private void DoRoll(float t_roll)
    {
       // Debug.Log("Do Roll");
        //Debug.Log(l_r);
        if (l_r < t_roll)
        {
            l_r += Time.deltaTime * roll_speed;
        }
        else
            rollDone = true;
    }

    /*private void Turn()
    {
        turn_amount += Time.deltaTime * turn_speed;

        //transform.rotation = Quaternion.Slerp(begin_rot, begin_rot * Quaternion.AngleAxis(pitch,-right) * Quaternion.AngleAxis(roll,-forward), turn_amount);
        transform.rotation = begin_rot * Quaternion.Euler(flip * Mathf.Clamp(l_p,0,pitch) + begin_rot.eulerAngles.x, 0, flip * Mathf.Clamp(l_r,0,roll) + begin_rot.eulerAngles.z);
        Debug.Log(begin_rot);
        Debug.Log(transform.rotation);
        Debug.Log(turn_amount);

        Debug.Log("Doing turn");


        if (l_p < pitch && l_r < roll)
        {
            l_p += Time.deltaTime * turn_speed;
            l_r += Time.deltaTime * turn_speed;
        }
        else
        {
            doTurn = false;
            turn_set = false;
            turn_time_local = turn_time;
            turn_amount = 0;
            flip *= -1;
        }

        if (Mathf.Abs(Quaternion.Angle(transform.rotation, begin_rot * Quaternion.AngleAxis(pitch, right) * Quaternion.AngleAxis(roll, -forward))) <= 0)
        {
            doTurn = false;
            turn_set = false;
            turn_time_local = turn_time;
            turn_amount = 0;
        }

    }*/
}
