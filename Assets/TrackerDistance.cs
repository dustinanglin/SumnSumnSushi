using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerDistance : MonoBehaviour {

    public float distance_multiplier = 0f;
    private float distance = 0f;
    private string dis1, dis2;
    private TextMesh num1, num2;
    private GameObject alien, player;

	// Use this for initialization
	void Start () {
        num1 = GameObject.Find("Num1").GetComponent<TextMesh>();
        num2 = GameObject.Find("Num2").GetComponent<TextMesh>();

        alien = GameObject.Find("AlienPoint");
        player = GameObject.Find("MotionTracker");
	}
	
	// Update is called once per frame
	void Update () {
        distance = Vector3.Distance(alien.transform.position, player.transform.position);
        float distance_dec = distance % 1;
        dis1 = distance.ToString("00");
        dis2 = distance_dec.ToString()[2] + "" + distance_dec.ToString()[3];
        num1.text = dis1;
        num2.text = dis2;
    }
}
