using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaserShoot : MonoBehaviour {

    private RaycastHit hit_spot;
    public float range, trigger_pull;
    public LineRenderer phaser_line;
    public GameObject phaser_location, phaser_hit_blob, spark_fountain;
    private OVRInput.Controller current_controller;
    private GameObject my_sparks;

    private Vector3 hit_location;

    private bool sparking = false;

    public float phaser_speed = 0f;


	// Use this for initialization
	void Start () {
        if (name.Contains("Right"))
        {
            current_controller = OVRInput.Controller.RTouch;
        }
        else
            current_controller = OVRInput.Controller.LTouch;
	}
	
	// Update is called once per frame
	void Update () {
        if (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, current_controller) > trigger_pull)
        {
            phaser_line.transform.gameObject.SetActive(true);

            if (Physics.Raycast(phaser_location.transform.position, -1 * transform.right, out hit_spot, range))
            {
                //Debug.Log(hit_spot.collider.name);
                hit_location = Vector3.MoveTowards(hit_location, hit_spot.point, phaser_speed);
                phaser_line.SetPosition(0, phaser_location.transform.position);
                phaser_line.SetPosition(1, hit_location);
                phaser_hit_blob.transform.position = hit_location;
                if (hit_spot.collider.name.Contains("Wall"))
                {
                    phaser_hit_blob.GetComponent<PhaserDetectorHit>().miss = true;
                }
                if (!sparking)
                {
                    sparking = true;
                    if (my_sparks)
                        Destroy(my_sparks);
                    my_sparks = Instantiate(spark_fountain, hit_spot.point, Quaternion.FromToRotation(hit_spot.point, hit_spot.normal), null);
                }
                if (sparking)
                {
                    my_sparks.transform.position = hit_spot.point;
                    my_sparks.transform.rotation = Quaternion.FromToRotation(hit_spot.point, hit_spot.normal);
                }
            }
        }
        else
        {
            phaser_line.transform.gameObject.SetActive(false);
            hit_location = phaser_location.transform.position;
            phaser_hit_blob.transform.position = phaser_location.transform.position;
            phaser_hit_blob.GetComponent<PhaserDetectorHit>().miss = false;
            my_sparks.GetComponent<ParticleSystem>().Stop();
            sparking = false;
        }

        Debug.DrawRay(transform.position, -1 * transform.right, Color.red);
	}
}
