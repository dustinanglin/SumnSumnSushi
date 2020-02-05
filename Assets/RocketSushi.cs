using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSushi : MonoBehaviour {

    private GameObject rocket_trail, rocket_glow;
    private List<GameObject> glows;
    private Rigidbody sushi_body;
    public float acceleration_rate;
    //public float max_rocket_time;
    public float pause_time;
    public float shake_intensity;
    public float shake_speed;
    private float rocket_time;
    private float speed;


	// Use this for initialization
	void Start () {
        rocket_glow = Instantiate((GameObject)Resources.Load("RocketStack"), transform.position, transform.rotation, transform);
        rocket_trail = Instantiate((GameObject)Resources.Load("Rocket"), transform.position, transform.rotation, transform);
        rocket_trail.transform.localPosition += new Vector3(0, 0, 5.42f);
        rocket_trail.SetActive(false);
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
        //speed = transform.forward;
    }
	
	// Update is called once per frame
	void Update () {
        if (rocket_time < pause_time)
        {
            
            sushi_body.MovePosition(transform.position + transform.right * Mathf.Sin(Time.time * shake_speed) * shake_intensity);
            shake_intensity += shake_intensity * Time.deltaTime;
        }

        if (rocket_time > pause_time)
        {
            rocket_trail.SetActive(true);
            foreach (GameObject child in glows)
            {
                child.SetActive(true);
            }
            speed += Time.deltaTime * acceleration_rate;
            sushi_body.velocity += -transform.forward * acceleration_rate;
        }

        rocket_time += Time.deltaTime;
    }
}
