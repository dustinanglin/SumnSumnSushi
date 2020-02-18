using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckFly : MonoBehaviour {

    private Animator animator;
    private DuckHunt director;

    private AudioSource[] duck_sounds;

    public float duck_speed, max_transition_time, min_transition_time, die_pause, fall_speed, death_floor, fly_away_ceiling;
    public int min_range, max_range;
    private int x_flip, y_flip; 
    public Vector2 upper_right_bounds, lower_left_bounds, quack_interval;
    private float x_vel, y_vel;
    private float trans_timer, quack_timer;
    private bool fall_sound, thud_sound, fly_away;

    public bool shot;

    private Vector3 collider_fly_size, collider_fly_center, collider_flyup_size, collider_flyup_center;
    private BoxCollider duck_collider;
    

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        director = GameObject.Find("DuckHuntDirector").GetComponent<DuckHunt>();
        duck_collider = GetComponent<BoxCollider>();
        duck_sounds = GetComponents<AudioSource>();

        trans_timer = Random.Range(min_transition_time, max_transition_time);
        quack_timer = Random.Range(quack_interval.x, quack_interval.y);

        x_flip = Random.value < .5f ? -1 : 1;
        y_flip = Random.value < .5f ? -1 : 1;

        x_vel = duck_speed * Random.Range(min_range, max_range) * x_flip;
        y_vel = duck_speed * Random.Range(min_range, max_range) * y_flip;

        collider_fly_size = new Vector3(.34f, .29f, 0);
        collider_fly_center = new Vector3(0, .015f, 0);

        collider_flyup_size = new Vector3(.32f, .29f, 0);
        collider_flyup_center = new Vector3(-.01f, -.005f, 0);

        fall_sound = false;
        thud_sound = false;
        fly_away = false;

	}
	
	// Update is called once per frame
	void Update () {
        if (!shot)
        {
            if (fly_away)
                doFlyAway();
            else
                doFly();
        }
        else
            doDie();
    }

    public void FlyAway()
    {
        fly_away = true;
    }

    private void doFlyAway()
    {
        animator.SetBool("duck_fly_away", true);
        x_vel = 0;
        y_vel = duck_speed * fall_speed;

        transform.Translate(x_vel, y_vel, 0);
        if (transform.position.y > fly_away_ceiling)
        {
            director.DuckOffScreen();
            duck_sounds[0].Stop();
        }
    }

    private void doDie()
    {
        director.DuckIsDead();
        if (die_pause > 0)
        {
            animator.SetBool("duck_shot", true);
            die_pause -= Time.deltaTime;
            duck_sounds[0].Stop();
        }
        else
        {
            animator.SetBool("duck_fall", true);
            transform.Translate(0, -fall_speed * duck_speed, 0);
            if (!fall_sound)
            {
                fall_sound = true;
                duck_sounds[2].Play();
            }
            if (transform.position.y < death_floor)
            {
                duck_sounds[2].Stop();
                if (!thud_sound)
                {
                    thud_sound = true;
                    duck_sounds[3].Play();
                    director.DuckOffScreen();
                }
            }
        }
    }

    private void doFly()
    {

        if (quack_timer > 0)
        {
            quack_timer -= Time.deltaTime;
        }
        else
        {
            quack_timer = Random.Range(quack_interval.x, quack_interval.y);
            duck_sounds[1].Play();
        }

        if (x_vel < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

        if (y_vel / duck_speed > max_range / 3)
        {
            animator.SetBool("fly_up", true);
            duck_collider.size = collider_flyup_size;
            duck_collider.center = collider_flyup_center;
        }
        else
        {
            animator.SetBool("fly_up", false);
            duck_collider.size = collider_fly_size;
            duck_collider.center = collider_fly_center;
        }


        if (trans_timer <= 0)
        {
            x_flip = Random.value < .5f ? -1 : 1;
            y_flip = Random.value < .5f ? -1 : 1;

            x_vel = duck_speed * Random.Range(min_range, max_range) * x_flip;
            y_vel = duck_speed * Random.Range(min_range, max_range) * y_flip;
            trans_timer = Random.Range(min_transition_time, max_transition_time);
        }

        //switch directions is leaving bounds
        if (transform.position.x > upper_right_bounds.x && x_vel > 0)
        {
            x_vel *= -1;
        }

        if (transform.position.x < lower_left_bounds.x && x_vel < 0)
        {
            x_vel *= -1;
        }

        if (transform.position.y > upper_right_bounds.y && y_vel > 0)
        {
            y_vel *= -1;
        }

        if (transform.position.y < lower_left_bounds.y && y_vel < 0)
        {
            y_vel *= -1;
        }


        transform.Translate(x_vel, y_vel, 0);
        trans_timer -= Time.deltaTime;
    }

    public void ScareDuck()
    {
        x_vel *= -1;
    }

}
