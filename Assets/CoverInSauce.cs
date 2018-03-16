using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverInSauce : MonoBehaviour {

    private Material sauce_material;
    private string sauce_type;

	// Use this for initialization
	void Start () {
        sauce_material = this.GetComponentInParent<SauceType>().sauced_sushi_material;
        sauce_type = this.GetComponentInParent<SauceType>().sauce_type;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log(other.name);

        if (other.GetComponent<Saucable>())
        {
            SetSushiSauce(other);
        }
    }

    private void SetSushiSauce(GameObject sushi)
    {

        if (sushi.GetComponent<Saucable>().sauce_type != sauce_type)
        {
            sushi.GetComponent<Saucable>().sauce_type = sauce_type;

            switch (sauce_type)
            {
                case "HotSauce":
                    foreach (Renderer rend in sushi.GetComponentsInChildren<Renderer>())
                    {
                        rend.material = sauce_material;
                    }
                    GameObject spicy_particles = Instantiate(GameObject.Find("SpicyParticles"));
                    spicy_particles.transform.parent = sushi.transform;
                    spicy_particles.transform.localPosition = Vector3.zero;
                    break;
            }
        } 
    }
}
