using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPress : MonoBehaviour {

    public float max_distance = .2f;
    private float start_y, start_x, start_z;
    public float spring_speed = 1;
    private Quaternion local_rot;
    private bool pressing = false;
    private bool pushed = false;
    private int push_count = 0;
    private Material buttonglow;
    private Material buttonnormal;
    private bool playing = false;

	// Use this for initialization
	void Start () {
        start_y = transform.localPosition.y;
        start_z = transform.localPosition.z;
        start_x = transform.localPosition.x;
        local_rot = transform.localRotation;
        buttonglow = Resources.Load("Plasticglow", typeof(Material)) as Material;
        buttonnormal = Resources.Load("Button", typeof(Material)) as Material;
     }
	
	// Update is called once per frame
	void Update () {
        
        if (!pressing)
        {
            transform.localPosition = new Vector3(0, start_y, 0);
        }
        transform.localPosition = new Vector3(start_x, Mathf.Clamp(transform.localPosition.y, -1 * max_distance + start_y, start_y), start_z);
        transform.localRotation = local_rot;

        //TextMesh temp = GameObject.Find("DebugText").GetComponent<TextMesh>();

        float percent_pushed = Mathf.Abs(start_y - transform.localPosition.y) / max_distance;

       // Debug.Log(percent_pushed);
        
        if (percent_pushed >= .6f && !pushed)
        {
            pushed = true;
            push_count++;
            this.GetComponent<Renderer>().material = buttonglow;
            //Debug.Log(push_count);
            //temp.text = "" + push_count;
            //      this.gameObject.GetComponent<Renderer>().material.
            
            //temp.text = temp2.name;
        }
        else if (percent_pushed <= .1f && pushed)
        {
            this.GetComponent<Renderer>().material = buttonnormal;
            pushed = false;
        }

        if (push_count >= 20 && !playing)
        {
            GameObject.Find("BeerFountain").GetComponent<ParticleSystem>().Play();
            playing = true;
        }

        /*if (transform.localPosition.y < start_y * 2 / 10 && !pushed)
        {
            Debug.Log("Button push");
            //this.gameObject.GetComponent<Renderer>().material.SetColor(Shader.Find())
            pushed = true;
        }
        else
            pushed = false;*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        pressing = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        pressing = false;
    }
}
