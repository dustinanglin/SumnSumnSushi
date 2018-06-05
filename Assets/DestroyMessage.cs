using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMessage : MonoBehaviour {

    public GameObject explode_red, explode_blue;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDisable()
    {
        GameObject.Find("WinDirector").GetComponent<TronGameDirector>().number_of_hex--;
        if (transform.Find("HexColor").GetComponent<Renderer>().material.name.Contains("Blue"))
        {
            //GameObject exploder = GameObject.Instantiate(GameObject.Find("HexTargetAnimate"), transform.position, GameObject.Find("HexTargetAnimate").transform.rotation);
            //exploder.GetComponent<Animation>().Play();
            Instantiate(explode_blue, transform.position, transform.rotation);
        }
        else
            Instantiate(explode_red, transform.position, transform.rotation);
    }
}
