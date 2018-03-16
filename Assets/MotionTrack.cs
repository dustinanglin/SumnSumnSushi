using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionTrack : MonoBehaviour {
    private GameObject zero, movespot, alien, tracker, player;
    public float distance = 0f;
    public float rotate = 0f;
    private float alien_distance, alien_rot = 0f;
    private Vector3 init;
  
    // Use this for initialization
    void Start () {
        zero = GameObject.Find("ZeroPoint");
        movespot = GameObject.Find("MoveSpot");
        alien = GameObject.Find("AlienPoint");
        tracker = GameObject.Find("MotionTracker");
        player = GameObject.Find("MouthCollider");
        init = movespot.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        alien_distance = -1 * Mathf.Clamp(Vector3.Distance(alien.transform.position, tracker.transform.position),.4f,10);

        Vector2 tracker_forward = new Vector2(tracker.transform.forward.x, tracker.transform.forward.z);
        Vector2 a1 = new Vector2(tracker.transform.position.x, tracker.transform.position.z);
        Vector2 a2 = new Vector2(alien.transform.position.x, alien.transform.position.z);
        Vector2 to_alien = a2 - a1;
 
        alien_rot = Vector2.SignedAngle(tracker_forward, to_alien);
        //Debug.Log(alien_rot);

        movespot.transform.position = init;
        movespot.transform.RotateAround(zero.transform.position, Vector3.forward, rotate * alien_rot);
        this.transform.position = Vector3.MoveTowards(movespot.transform.position, zero.transform.position, distance * alien_distance);

        
	}
}
