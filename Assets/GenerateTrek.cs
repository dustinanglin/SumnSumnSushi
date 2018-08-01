using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTrek : MonoBehaviour {


    private bool generate_communicator;
    public float force;
    public float offset;
    private Vector3 face_offset, collider_spot;

	// Use this for initialization
	void Start () {
        generate_communicator = true;
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(collider_spot, face_offset, Color.cyan);
	}

    private void OnTriggerEnter(Collider other)
    {

        Saucable fish_sauce = null;

        if (other.gameObject.GetComponent<Saucable>())
        {
            fish_sauce = other.gameObject.GetComponent<Saucable>();
        }
        else if (other.gameObject.transform.parent != null)
        {
            fish_sauce = other.gameObject.transform.parent.gameObject.GetComponent<Saucable>();
            Debug.Log("Sauce is" + fish_sauce.sauce_type);
        }


        if (fish_sauce != null)
        {
            if (generate_communicator && fish_sauce.sauce_type.Contains("Trek"))
            {
                collider_spot = other.transform.position;
                Debug.Log(other.name + " Contains Trek");
                face_offset = Vector3.MoveTowards(other.transform.position, transform.position, -1 * offset);
                GameObject badge = Instantiate(GameObject.Find("Combadge"), face_offset, transform.rotation, null);
                Vector3 move_force = (other.transform.position - transform.position) * force;
                badge.GetComponent<Rigidbody>().AddForce(move_force);
                //generate_communicator = false;
            }
        }
    }
}
