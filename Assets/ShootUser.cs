using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootUser : MonoBehaviour {

    private Animation myAnim;
    private BulletGenerator myGun;
    private AnimationState myClip, dieClip;
    public float time_between_shots = 5;
    public float initial_delay_time = 0;
    public float shoot_delay_time = 0;
    public float die_ease = 1;
    private float die_timer = 1;
    private float shottimer = 1;
    private bool firstplay = true;
    private bool fired = false;
    private bool shot = false;
    private TimeManipulator timeMan;


	// Use this for initialization
	void Start () {
        myAnim = GetComponent<Animation>();
        myGun = GetComponentInChildren<BulletGenerator>();
        myClip = myAnim["StandAndShoot"];
        if (myAnim["GotShot"])
            dieClip = myAnim["GotShot"];
        timeMan = GameObject.Find("TimeController").GetComponent<TimeManipulator>();
    }
	
	// Update is called once per frame
	void Update () {
        myClip.speed = timeMan.timescale;

        if (initial_delay_time <= 0)
        {
            if (firstplay)
            {
                myAnim.Play();
                firstplay = false;
            }

            if (myClip.time >= 4 && shoot_delay_time > 0)
            {
                shoot_delay_time -= Time.deltaTime * timeMan.timescale;
                myClip.speed = 0;
            }

            if (shoot_delay_time <= 0 && !shot)
            {
                Shoot();
            }

            if (shot)
            {
                Die();
            }
                


        }
        else
            initial_delay_time -= Time.deltaTime * timeMan.timescale;

        //Debug.Log(delay_time);
	}

    private void Die()
    {
        if (!myAnim.isPlaying) {
            GameObject.Find("WinCondition").GetComponent<WinDetector>().increment_death();
            Destroy(this.gameObject);
        }
        dieClip.speed = timeMan.timescale;
    }

    public void getShot()
    {
        if (!shot)
        {
            shot = true;
            myAnim.clip = dieClip.clip;
            dieClip.speed = timeMan.timescale;
            dieClip.time = 0;
            myAnim.Play();
            die_timer = 1; 
        }
    }

    private void Shoot()
    {
        if (myClip.time >= 4.7f && !fired)
        {
            fired = true;
            myGun.Fire(10);
        }

        if (fired)
            shottimer += Time.deltaTime * timeMan.timescale;

        if (shottimer >= time_between_shots)
        {
            shottimer = 0;
            fired = false;
            myClip.time = 4;
            myAnim.Play();
        }
    }
}
