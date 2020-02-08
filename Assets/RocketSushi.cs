using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSushi : MonoBehaviour {

    private GameObject rocket_trail, rocket_glow, rocket_sparks, explosion;
    private List<GameObject> glows;
    private Rigidbody sushi_body;
    public float acceleration_rate;
    public float max_rocket_time;
    public float pause_time;
    public float shake_intensity;
    public float shake_speed;
    private float rocket_time;
    private float speed;


	// Use this for initialization
	void Start () {
        //Add rocket engines & sparks
        rocket_glow = Instantiate((GameObject)Resources.Load("RocketStack"), transform.position, transform.rotation, transform);
        rocket_sparks = Instantiate((GameObject)Resources.Load("PS_Engine_Sparks"), transform.position, transform.rotation, transform);
        rocket_sparks.transform.localPosition += new Vector3(0, 0, 2.1f);

        //Add rocket particle effects
        rocket_trail = Instantiate((GameObject)Resources.Load("Rocket"), transform.position, transform.rotation, transform);
        rocket_trail.transform.localPosition += new Vector3(0, 0, 5.42f);
        rocket_trail.SetActive(false);

        //explosion effects
        explosion = (GameObject)Resources.Load("Single_Firework_Blue");

        //sushi becomes zero g
        sushi_body = GetComponent<Rigidbody>();
        sushi_body.useGravity = false;

        glows = new List<GameObject>();

        foreach (Transform rocket in rocket_glow.transform)
        {
            foreach (Transform child in rocket)
            {
                if (child.gameObject.name.Contains("Disc_1"))
                {
                    glows.Add(child.gameObject);
                    child.gameObject.SetActive(false);
                }
            }
        }

        rocket_time = 0;
        speed = 0;
        max_rocket_time += pause_time;
        //speed = transform.forward;
    }
	
	// Update is called once per frame
	void Update () {
        if (rocket_time < pause_time)
        {
            sushi_body.MoveRotation(transform.localRotation * Quaternion.AngleAxis(Mathf.Sin(Time.time * shake_speed) * shake_intensity, transform.up));
            //sushi_body.MovePosition(transform.position + transform.right * Mathf.Sin(Time.time * shake_speed) * shake_intensity);
            shake_intensity += shake_intensity * Time.deltaTime;
        }

        if (rocket_time > pause_time)
        {
            rocket_trail.SetActive(true);
            rocket_sparks.SetActive(false);
            foreach (GameObject child in glows)
            {
                child.SetActive(true);
            }
            speed += Time.deltaTime * acceleration_rate;
            sushi_body.velocity += -transform.forward * acceleration_rate;
        }

        if (rocket_time > max_rocket_time)
        {
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }

        rocket_time += Time.deltaTime;
    }
}
