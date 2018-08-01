using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSpheres : MonoBehaviour {

    public int number_of_sphere = 0;
    private bool initiated, initiate = false;
    public GameObject sphere;
    private List<GameObject> spheres;

	// Use this for initialization
	void Start () {
        spheres = new List<GameObject>(); 
	}
	
	// Update is called once per frame
	void Update () {
		if (initiate && !initiated)
        {
            initiateSequence();
        }
	}

    public void setInitiate()
    {
        initiate = true;
    }

    public bool isInitiated()
    {
        return initiated;
    }

    private void initiateSequence()
    {
        for (int i = 0; i < number_of_sphere; i++)
        {
            spheres.Add(Instantiate(sphere, transform.position, transform.rotation, null));
        }
        GetComponent<AudioSource>().Play();
        initiated = true;
    }

    public void endGame()
    {
        foreach (GameObject sph in spheres)
        {
            Destroy(sph);
        }

        initiated = false;
        initiate = false;
    }
}
