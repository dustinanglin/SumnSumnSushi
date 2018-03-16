using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGenerator : MonoBehaviour {
    public float bulletSpeed = 100;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawRay(transform.position, -1 * transform.forward, Color.blue);
	}

    public void Fire()
    {
        GameObject Bullet = Instantiate(GameObject.Find("SushiBullet"),this.transform.position, this.transform.rotation);
        Bullet.AddComponent<BulletMove>().Fire(bulletSpeed);
    }

    public void Fire(float trailLength)
    {
        GameObject Bullet = Instantiate(GameObject.Find("SushiBullet"), this.transform.position, this.transform.rotation);
        Bullet.GetComponent<TrailRenderer>().time = trailLength;
        Bullet.AddComponent<BulletMove>().Fire(bulletSpeed);
    }
}
