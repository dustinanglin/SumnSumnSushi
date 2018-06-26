using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTrek : MonoBehaviour {


    private bool generate_communicator;
    public Vector3 force;
    public Vector3 offset;

	// Use this for initialization
	void Start () {
        generate_communicator = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {

        Saucable fish_sauce = new Saucable();

        if (other.gameObject.transform.parent != null)
        {
            fish_sauce = other.gameObject.transform.parent.gameObject.GetComponent<Saucable>();
            Debug.Log("Sauce is" + fish_sauce.sauce_type);
        }


        if (fish_sauce != null)
        {
            if (generate_communicator && fish_sauce.sauce_type.Contains("Trek"))
            {
                Debug.Log(other.name + " Contains Trek");
                GameObject badge = Instantiate(GameObject.Find("Combadge"), other.transform.position + offset, other.transform.rotation, null);
                badge.GetComponent<Rigidbody>().AddForce(force);
                //generate_communicator = false;
            }
        }
    }
}
