using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TronGameDirector : MonoBehaviour {

    public int number_of_hex;
    public float end_time_delay = 0f;
    private bool do_final_fireworks = true;
    public bool test_end = false;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (number_of_hex <= 0 || test_end)
        {
            if (do_final_fireworks)
            {
                do_final_fireworks = false;
                Debug.Log("Doing Final Fireworks");
                GameObject finalred = GameObject.Find("FinalRedHolder");
                GameObject finalblue = GameObject.Find("FinalBlueHolder");
                GameObject.Instantiate(GameObject.Find("FinalRed"), finalred.transform.position, finalred.transform.rotation, finalred.transform);
                GameObject.Instantiate(GameObject.Find("FinalBlue"), finalblue.transform.position, finalblue.transform.rotation, finalblue.transform);
                finalred.GetComponent<AudioSource>().Play();
            }

            if (end_time_delay >= 0)
                end_time_delay -= Time.deltaTime;
            else
                SceneManager.LoadScene("SushiHub", LoadSceneMode.Single);
        }
	}


}
