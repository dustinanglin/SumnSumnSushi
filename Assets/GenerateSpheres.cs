using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSpheres : MonoBehaviour {

    public int number_of_sphere = 0;
    public GameObject sphere;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < number_of_sphere; i++)
        {
            Instantiate(sphere, transform.position, transform.rotation, null);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
