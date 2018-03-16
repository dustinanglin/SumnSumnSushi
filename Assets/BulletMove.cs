using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour {
    public float move_speed = 1;
    public bool fire = false;
    public Vector3 forward;
    private TimeManipulator timeMan;

	void Start () {
        //Destroy(this.gameObject, 20f);
        timeMan = GameObject.Find("TimeController").GetComponent<TimeManipulator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (fire)
        {
            transform.Translate(-1 * transform.forward * Time.deltaTime * timeMan.timescale, Space.World);
        }
        Debug.DrawRay(transform.position, -1 * transform.forward, Color.red);
	}

    public void Fire(float bulletspeed)
    {
        move_speed = bulletspeed;
        fire = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponentInParent<ShootUser>())
        {
            Debug.Log("Hit Bad Guy");
            Destroy(this.gameObject);
        }
    }
}
