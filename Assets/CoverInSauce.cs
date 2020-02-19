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

            if (sauce_material)
            {
                foreach (Renderer rend in sushi.GetComponentsInChildren<Renderer>())
                {
                    rend.material = sauce_material;
                }
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

                case "MonsterSauce":
                    //call dart function
                    //TransformToDart(sushi);
                    TransformToMonster(sushi);
                    break;

                case "SciFiSauce":
                    LaunchSushi(sushi);
                    break;

                case "DigitalSauce":
                    DigitalTransformation(sushi);
                    break;

                default:
                    break;

            }
        } 
    }

    private void LaunchSushi(GameObject sushi)
    {
        RocketSushi rocket_temp = sushi.AddComponent<RocketSushi>();
        rocket_temp.acceleration_rate = .01f;
        rocket_temp.pause_time = 3;
        rocket_temp.max_rocket_time = 3;
        rocket_temp.shake_intensity = .2f;
        rocket_temp.shake_speed = 75;
    }

    private void TransformToMonster(GameObject sushi)
    {
        Instantiate((GameObject)Resources.Load("MonsterParts"), sushi.transform);
        MonsterJump monster_temp = sushi.AddComponent<MonsterJump>();
        monster_temp.jump_delay = 1;
        monster_temp.jump_strength = 10;
        monster_temp.jump_offset = new Vector3(0, .1f, 0);
        monster_temp.force_radius = .5f;
        monster_temp.do_jump = true;
    }

    private void DigitalTransformation(GameObject sushi)
    {
        foreach (Transform child in sushi.transform)
        {
            child.gameObject.AddComponent<MeshSquare>().emission_multiplier = 3;
        }
    }

    private void TransformToDart(GameObject sushi)
    {
        Vector3 center;
        float half_distance;
        if (sushi.name.Contains("Roe"))
        {
            center = sushi.GetComponent<Collider>().bounds.center;
            half_distance = sushi.GetComponent<Collider>().bounds.extents.x;
        }
        else
        {
            center = sushi.transform.Find("Rice").GetComponent<Collider>().bounds.center;
            half_distance = sushi.transform.Find("Rice").GetComponent<Collider>().bounds.extents.x;
        }

   
        GameObject fins = Instantiate((GameObject)Resources.Load("Fins"), sushi.transform.position, Quaternion.LookRotation(sushi.transform.right,sushi.transform.up), sushi.transform);
        float fin_half_distance = fins.GetComponentInChildren<Collider>().bounds.extents.x;
        fins.transform.Rotate(-30, 0, 0, Space.Self);
        fins.transform.Translate(-1 * (half_distance + fin_half_distance - (fin_half_distance/5f)), 0, 0);

        GameObject needle = Instantiate((GameObject)Resources.Load("Needle"), sushi.transform.position, Quaternion.LookRotation(sushi.transform.right, sushi.transform.up), sushi.transform);
        float needle_half_distance = needle.GetComponent<Collider>().bounds.extents.y;
        needle.transform.Translate(half_distance + needle_half_distance - (needle_half_distance/5f), 0, 0);
        needle.transform.Rotate(0, 0, -90, Space.Self);
    }
}
