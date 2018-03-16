using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatGenerator : MonoBehaviour {

    public float timeInterval;
    private float localInterval;
    private GameObject boat;
    private TimeManipulator timeMan;
    public Vector3 fish_offset;
    public Vector3 fish_offrotation = new Vector3(0, 90, 0);
    private Vector3 fish_position, fish_rotation;
    public float boat_prime = 100;

	// Use this for initialization
	void Start () {
        boat = GameObject.Find("SushiBoat");
        localInterval = 1;

        fish_position = transform.position + new Vector3(.01f, -.02f, 0);
        fish_rotation = transform.rotation * fish_offrotation;

        TrumpThePump(boat_prime);
    }
	
	// Update is called once per frame
	void Update () {
        if (localInterval >= 0)
        {
            localInterval -= Time.deltaTime;
        }
        else
        {
            CreateFish(0, 0);
            localInterval = timeInterval;
        }      
	}

    private void CreateFish(float x_offset, float time_offset)
    {
        GameObject temp_boat = Instantiate(boat, transform.position + new Vector3(x_offset, 0, 0), transform.rotation);
        Boatmove boatmove = temp_boat.AddComponent<Boatmove>();
        boatmove.time = time_offset;

        int fishtype = Random.Range(0, 5);
        //Debug.Log("Boat Create");
        Vector3 temp = fish_position + new Vector3(x_offset, 0, 0);
        //Debug.Log("Fish Position:" + temp);
        //Debug.Log("Boat Position:" + temp_boat.transform.position);
        switch (fishtype)
        {
            case 0:
                Instantiate(GameObject.Find("RoeBoat"), fish_position + new Vector3(x_offset, 0, 0), Quaternion.Euler(fish_rotation));
                break;
            case 1:
                Instantiate(GameObject.Find("Yellowfish"), fish_position + new Vector3(x_offset, 0, 0), Quaternion.Euler(fish_rotation));
                break;
            case 2:
                Instantiate(GameObject.Find("Tuna"), fish_position + new Vector3(x_offset, 0, 0), Quaternion.Euler(fish_rotation));
                break;
            case 3:
                Instantiate(GameObject.Find("Salmon"), fish_position + new Vector3(x_offset, 0, 0), Quaternion.Euler(fish_rotation));
                break;
            case 4:
                Instantiate(GameObject.Find("Shrimp"), fish_position + new Vector3(x_offset, 0, 0), Quaternion.Euler(fish_rotation));
                break;
        }
    }

    private void TrumpThePump(float time)
    {
        localInterval = Mathf.RoundToInt(time) % Mathf.RoundToInt(timeInterval);
        int boat_num = Mathf.RoundToInt(time) / Mathf.RoundToInt(timeInterval);
        //Debug.Log("boatnum:" + boat_num + " remainder:" + localInterval);
        
        for (int i=boat_num; i >= 0; i--)
        {
            float future_time = i * timeInterval + localInterval;
            CreateFish(future_time * .05f, future_time);
        }
    }
}
