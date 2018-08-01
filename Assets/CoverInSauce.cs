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

            foreach (Renderer rend in sushi.GetComponentsInChildren<Renderer>())
            {
                rend.material = sauce_material;
            }

            switch (sauce_type)
            {
                case "HotSauce":
                    
                    GameObject spicy_particles = Instantiate(GameObject.Find("SpicyParticles"));
                    spicy_particles.transform.parent = sushi.transform;
                    spicy_particles.transform.localPosition = Vector3.zero;
                    break;

                case "XenoSauce":
                    break;

                case "TronSauce":
                    break;

                case "TrekSauce":
                    GameObject trek_particles = Instantiate((GameObject)Resources.Load("TrekSauceParticles"), sushi.transform.position, sushi.transform.rotation, sushi.transform);
                    trek_particles.transform.localPosition = new Vector3(0, 0, -1.6f);
                    Debug.Log(trek_particles + "was created");
                    break;

                default:
                    break;

            }
        } 
    }
}
