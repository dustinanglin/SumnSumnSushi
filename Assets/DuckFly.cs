using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckFly : MonoBehaviour {

    private Animator animator;
    public float duck_speed, max_transition_time, min_transition_time, die_pause, fall_speed;
    public int min_range, max_range;
    private int x_flip, y_flip; 
    public Vector2 upper_right_bounds, lower_left_bounds;
    private float x_vel, y_vel;
    private float trans_timer;
    public bool shot;
    

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();

        trans_timer = Random.Range(min_transition_time, max_transition_time);


        x_flip = Random.value < .5f ? -1 : 1;
        y_flip = Random.value < .5f ? -1 : 1;

        x_vel = duck_speed * Random.Range(min_range, max_range) * x_flip;
        y_vel = duck_speed * Random.Range(min_range, max_range) * y_flip;
	}
	
	// Update is called once per frame
	void Update () {
        if (!shot)
            doFly();
        else
            doDie();
    }

    private void doDie()
    {
        if (die_pause > 0)
        {
            animator.SetBool("duck_shot", true);
            die_pause -= Time.deltaTime;
        }
        else
        {
            animator.SetBool("duck_fall", true);
            transform.Translate(0, -fall_speed * duck_speed, 0);
        }
    }

    private void doFly()
    {
        if (x_vel < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else
            transform.localScale = new Vector3(1, 1, 1);

        if (y_vel / duck_speed > max_range / 3)
            animator.SetBool("fly_up", true);
        else
            animator.SetBool("fly_up", false);


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

}
