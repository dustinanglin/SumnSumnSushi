using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PointBallMover : MonoBehaviour {

    public float range_x, range_y, range_z, speed;
    public Material material;
    private bool is_moving = false;
    private Vector3 new_location, start;

    // Use this for initialization
    void Start () {
        start = transform.position;

        GameObject child = transform.GetChild(0).gameObject;
        Renderer rend = child.GetComponent<Renderer>();
        Material new_mat = new Material(material);
        new_mat.SetFloat("_Random_seed", Random.Range(0, 255.0f));
        rend.material = new_mat;
    }
	
	// Update is called once per frame
	void Update () {
		if (is_moving)
        {
            transform.position = Vector3.MoveTowards(transform.position, new_location, speed);
        
            if (Vector3.Distance(transform.position, new_location) < .1f)
            {
                is_moving = false;
            }
        }
        else
        {
            new_location = new Vector3(Random.Range(start.x - range_x, start.x + range_x), Random.Range(start.y - range_y, start.y + range_y), Random.Range(start.z - range_z, start.z + range_z));
            is_moving = true;
        }
	}
}
