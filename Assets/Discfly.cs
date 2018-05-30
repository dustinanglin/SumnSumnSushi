using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Discfly : MonoBehaviour {

    public float disc_speed, rotate_speed, return_speed, max_throw_speed, trigger_release_limit = 1;
    public float release_delay, min_throw_speed = 0;


    private float m_release_delay, lerp_time = 0;
    private float distance_from_home = 1;
    private bool throw_disc, throwing, home, delay_throw, reset, return_home = false;
    private float throw_velocity = 0;
    private Quaternion new_rotation, start_rot;
    private Vector3 start_pos, throw_dir;
    public GameObject right_hand;
    private ThrowSpeed Thrower;


	// Use this for initialization
	void Start () {
        Thrower = right_hand.GetComponent<ThrowSpeed>();
        m_release_delay = release_delay;
        home = false;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, transform.forward, Color.red);
        Debug.DrawRay(transform.position, transform.up, Color.blue);

        throw_disc = (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) < trigger_release_limit);

        reset = OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch);

        Debug.DrawRay(transform.position, Vector3.Project(Thrower.m_velocity, transform.forward), Color.yellow);

        //home = (transform.position == right_hand.transform.position);

        if (throw_disc && !throwing && !return_home)
        {
            if (home && !delay_throw)
            {
                delay_throw = true;
                throw_velocity = Mathf.Clamp(Vector3.Magnitude(Thrower.m_velocity),0,max_throw_speed);
                if (throw_velocity <= min_throw_speed)
                {
                    throw_dir = Thrower.m_velocity;

                    throw_velocity = Mathf.Clamp(throw_velocity,0,.01f);
                }
                else
                {
                    throw_dir = Thrower.m_velocity;
                    transform.rotation = Quaternion.LookRotation(throw_dir, transform.up);
                }
            }
            /*else
            {
                throwing = false;
                return_home = true;
                /*m_release_delay = release_delay;
                transform.parent = right_hand.transform;
                transform.position = right_hand.transform.position;
                transform.rotation = right_hand.transform.rotation;
            }*/
        }

        if (!throw_disc && !home)
        {
            return_home = true;
            if (throwing)
            {
                distance_from_home = Vector3.Distance(transform.position, right_hand.transform.position);
                start_pos = transform.position;
                start_rot = transform.rotation;
                lerp_time = 0;
            }
            throwing = false;
           // Debug.Log("Return Home!");
        }

        if (delay_throw)
        {
            if (m_release_delay > 0)
            {
                m_release_delay -= Time.deltaTime;
            }
            else
            {

                throwing = true;
                home = false;
                transform.parent = null;
                delay_throw = false;
            }
        }

        if (return_home)
        {
            lerp_time += (Time.deltaTime * return_speed);
            //Debug.Log("Returning to home...");
            transform.rotation = Quaternion.Slerp(start_rot, right_hand.transform.rotation, lerp_time);
            transform.position = Vector3.MoveTowards(transform.position, right_hand.transform.position, Mathf.Clamp(throw_velocity,return_speed,Mathf.Infinity) * disc_speed * Time.deltaTime);
            float distance_away = Vector3.Distance(transform.position, right_hand.transform.position);
            float start_dist_away = Vector3.Distance(start_pos, right_hand.transform.position);


            if (distance_away >= .1f && start_dist_away > 0f)
            {
                float percent_away = (start_dist_away - distance_away) / start_dist_away;
                int sound_strength = Mathf.RoundToInt(percent_away * 180);

                //int sound_strength = Mathf.Clamp(255 / Mathf.RoundToInt(Mathf.Clamp(Vector3.Distance(transform.position, right_hand.transform.position),1f,Mathf.Infinity)),1,255);
                Debug.Log("Sound strength: " + sound_strength);
                byte[] sound = {(byte)sound_strength, (byte)sound_strength , (byte)sound_strength , (byte)sound_strength , (byte)sound_strength };
                OVRHapticsClip temp = new OVRHapticsClip(sound, sound.Length);
                OVRHaptics.RightChannel.Preempt(temp);
            }
           


            if (Vector3.Distance(transform.position,right_hand.transform.position) < .1f)
            {
                home = true;
                return_home = false;
                byte[] sound = { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255 };
                OVRHapticsClip temp = new OVRHapticsClip(sound, sound.Length);
                OVRHaptics.RightChannel.Preempt(temp);
                transform.parent = right_hand.transform;
                transform.position = right_hand.transform.position;
                transform.rotation = right_hand.transform.rotation;
                m_release_delay = release_delay;

            }
        }

        if (throwing)
        {
            transform.Translate(Vector3.ClampMagnitude(throw_dir,1) * Time.deltaTime * Mathf.Clamp(disc_speed * throw_velocity,0,max_throw_speed), Space.World);
        }

        if (reset)
        {
            throwing = false;
            home = true;
            m_release_delay = release_delay;
            transform.parent = right_hand.transform;
            transform.position = right_hand.transform.position;
            transform.rotation = right_hand.transform.rotation;
        }

	}


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collided with" + other.gameObject.name);
        //Debug.Log("Current forward: " + transform.forward);

        if (!other.name.Contains("Right"))
        {
            Vector3 col_normal;
            Vector3 new_up;
            float angle_to_home;

            GameObject player = GameObject.Find("ForwardDirection");


            if (other.gameObject.name.Contains("Plane"))
            {
                col_normal = other.gameObject.transform.up;
                new_up = -1 * Vector3.Reflect(transform.up, col_normal);
            }
            else
            {
                col_normal = other.gameObject.transform.right;
                new_up = Vector3.Reflect(transform.up, col_normal);
            }
            Vector3 new_direction = Vector3.Reflect(transform.forward, col_normal);
            // Debug.Log("Rotate to new forward: " + new_direction);


            new_rotation = Quaternion.LookRotation(new_direction, new_up);
            transform.rotation = new_rotation;
            throw_dir = transform.forward;

            Vector2 disc_plane = new Vector2(transform.forward.x, transform.forward.z);
            Vector2 world_plane = new Vector2(player.transform.forward.x, player.transform.forward.z);

            angle_to_home = Vector2.SignedAngle(disc_plane, world_plane);

            //Debug.Log("Angle to user: " + disc_plane);

            RaycastHit hit;
            if (!Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
            {
                Vector3 toward_user = player.transform.position - transform.position;
                new_rotation = Quaternion.LookRotation(toward_user);
                transform.rotation = new_rotation;
                throw_dir = transform.forward;
            }
            //Debug.Log("Bounce");
        }

       /*if (transform.forward.z < 0f)
        {
            Vector3 toward_user = player.transform.position - transform.position;
            new_rotation = Quaternion.LookRotation(toward_user);
            transform.rotation = new_rotation;
        }*/

       

        //Debug.Log("New forward: " + transform.forward);


    }

    /* {
     Debug.Log("Collider with" + other.gameObject.name);

        Vector3 col_normal = collision.contacts[0].normal;
        Vector3 new_direction = Vector3.Reflect(transform.forward, col_normal);
        transform.rotation.SetLookRotation(new_direction);

     }*/


    private void OnTriggerStay(Collider other)
    {
        /*if (other.name.Contains("Right"))
        {
            if (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, OVRInput.Controller.RTouch) >= .1f)
            {
                throwing = false;
                transform.parent = right_hand.transform;
                transform.position = right_hand.transform.position;
                transform.rotation = right_hand.transform.rotation;
            }
        }*/
    }
}
