using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TronGameDirector : MonoBehaviour {

    public int number_of_hex;
    public float end_time_delay = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (number_of_hex <= 0)
        {
            if (end_time_delay >= 0)
                end_time_delay -= Time.deltaTime;
            else
                SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
	}
}
