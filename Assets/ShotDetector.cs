using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotDetector : MonoBehaviour {

    private ShootUser myUser;
    private Transform myChest;

	// Use this for initialization
	void Start () {
        myUser = GetComponent<ShootUser>();
        myChest = this.gameObject.transform.Find("Pelvis/Stomach/Lower_Chest");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.gameObject.name.Contains("Bullet"))
        {
            myUser.getShot();
            GameObject.Instantiate(GameObject.Find("Bloodsplatter"), myChest.position, GameObject.Find("Bloodsplatter").transform.rotation).GetComponent<Bloodcontrol>().bleeding = true;
            Debug.Log("Got Shot!");
        }
    }
}
