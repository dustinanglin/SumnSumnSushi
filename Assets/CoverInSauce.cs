using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverInSauce : MonoBehaviour {

    private Material sauce_material;
    private string sauce_type;
    private GameObject combo_effect;
    private GameObject dish_sauce;

    private Material hot, xeno, tron, trek, monster, scifi, digital, target, pokemon, godzilla;
    private AudioSource new_combo_yay, combo_poof;

	// Use this for initialization
	void Start () {

        hot = GameObject.Find("HotSauce").GetComponent<SauceType>().sauced_sushi_material;
        xeno = GameObject.Find("XenoSauce").GetComponent<SauceType>().sauced_sushi_material;
        tron = GameObject.Find("TronSauce").GetComponent<SauceType>().sauced_sushi_material;
        trek = GameObject.Find("TrekSauce").GetComponent<SauceType>().sauced_sushi_material;
        monster = GameObject.Find("MonsterSauce").GetComponent<SauceType>().sauced_sushi_material;
        scifi = GameObject.Find("SciFiSauce").GetComponent<SauceType>().sauced_sushi_material;
        digital = GameObject.Find("DigitalSauce").GetComponent<SauceType>().sauced_sushi_material;
        target = GameObject.Find("TargetSauce").GetComponent<SauceType>().sauced_sushi_material;

        combo_effect = GameObject.Find("SauceComboParticles");
        dish_sauce = GameObject.Find("DishSauce");
        new_combo_yay = dish_sauce.GetComponents<AudioSource>()[0];
        combo_poof = dish_sauce.GetComponents<AudioSource>()[1];

        if (this.name.Contains("Dish"))
        {
            sauce_material = this.GetComponent<SauceType>().sauced_sushi_material;
            sauce_type = this.GetComponent<SauceType>().sauce_type;
        }
        else
        {
            sauce_material = this.GetComponentInParent<SauceType>().sauced_sushi_material;
            sauce_type = this.GetComponentInParent<SauceType>().sauce_type;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (this.name.Contains("Dish"))
        {
            sauce_material = this.GetComponent<SauceType>().sauced_sushi_material;
            sauce_type = this.GetComponent<SauceType>().sauce_type;
        }
        else
        {
            sauce_material = this.GetComponentInParent<SauceType>().sauced_sushi_material;
            sauce_type = this.GetComponentInParent<SauceType>().sauce_type;
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        //Debug.Log(other.name);

        if (other.GetComponent<Saucable>())
        {
            SetSushiSauce(other);
        }
        else if (other.GetComponentInChildren<Saucable>())
        {
            foreach (Transform child in other.transform)
            {
                if (child.name.Contains("Dish"))
                {
                    SetDishSauce(child.gameObject);
                    Debug.Log("Dish Sauce Set");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if (other.gameObject.GetComponent<Saucable>())
        {
            SetSushiSauce(other.gameObject);
        }
        else if (other.gameObject.GetComponentInParent<Saucable>())
        {
            SetSushiSauce(other.gameObject.transform.parent.gameObject);
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
                    
                    GameObject spicy_particles = Instantiate(GameObject.Find("SpicyParticles"));
                    spicy_particles.transform.parent = sushi.transform;
                    spicy_particles.transform.localPosition = Vector3.zero;
                    SauceObject(sushi, sauce_material);
                    break;

                case "XenoSauce":
                    SauceObject(sushi, sauce_material);
                    break;

                case "TronSauce":
                    SauceObject(sushi, sauce_material);
                    break;

                case "TrekSauce":
                    GameObject trek_particles = Instantiate((GameObject)Resources.Load("TrekSauceParticles"), sushi.transform.position, sushi.transform.rotation, sushi.transform);
                    trek_particles.transform.localPosition = new Vector3(0, 0, -1.6f);
                    Debug.Log(trek_particles + "was created");
                    SauceObject(sushi, sauce_material);
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

                case "TargetSauce":
                    SauceObject(sushi, sauce_material);
                    break;

                default:
                    break;

            }
        } 
    }

    private void SauceObject(GameObject saucedObject, Material sauceMaterial)
    {
        if (sauceMaterial)
        {
            foreach (Renderer rend in saucedObject.GetComponentsInChildren<Renderer>())
            {
                if (!rend.gameObject.name.Contains("Particle"))
                    rend.material = sauceMaterial;
            }
        }
    }

    private string GetComboType(string existing_sauce, string new_sauce)
    {
        switch (existing_sauce)
        {
            case "HotSauce":
                if (new_sauce.Contains("HotSauce") || new_sauce.Contains("TargetSauce") || new_sauce.Contains("DigitalSauce"))
                    return "HotSauce";
                else
                    return new_sauce;

            case "XenoSauce":
                if (new_sauce.Contains("XenoSauce") || new_sauce.Contains("SciFiSauce") || new_sauce.Contains("MonsterSauce"))
                    return "XenoSauce";
                else return new_sauce;

            case "TronSauce":
                if (new_sauce.Contains("TronSauce") || new_sauce.Contains("SciFiSauce") || new_sauce.Contains("DigitalSauce"))
                    return "TronSauce";
                else return new_sauce;

            case "TrekSauce":
                if (new_sauce.Contains("TrekSauce") || new_sauce.Contains("TargetSauce") || new_sauce.Contains("ScifiSauce"))
                    return "TrekSauce";
                else return new_sauce;

            case "MonsterSauce":
                if (new_sauce.Contains("MonsterSauce"))
                    return new_sauce;

                if (new_sauce.Contains("SciFiSauce"))
                    return "XenoSauce";

                if (new_sauce.Contains("TargetSauce"))
                    return "GodzillaSauce";

                if (new_sauce.Contains("DigitalSauce"))
                    return "PokemonSauce";

                else return new_sauce;

            case "SciFiSauce":
                if (new_sauce.Contains("MonsterSauce"))
                    return "XenoSauce";

                if (new_sauce.Contains("SciFiSauce"))
                    return new_sauce;

                if (new_sauce.Contains("TargetSauce"))
                    return "TrekSauce";

                if (new_sauce.Contains("DigitalSauce"))
                    return "TronSauce";

                else return new_sauce;

            case "DigitalSauce":
                if (new_sauce.Contains("MonsterSauce"))
                    return "PokemonSauce";

                if (new_sauce.Contains("SciFiSauce"))
                    return "TronSauce";

                if (new_sauce.Contains("TargetSauce"))
                    return "HotSauce";

                if (new_sauce.Contains("DigitalSauce"))
                    return new_sauce;

                else return new_sauce;

            case "TargetSauce":
                if (new_sauce.Contains("MonsterSauce"))
                    return "GodzillaSauce";

                if (new_sauce.Contains("SciFiSauce"))
                    return "TrekSauce";

                if (new_sauce.Contains("TargetSauce"))
                    return new_sauce;

                if (new_sauce.Contains("DigitalSauce"))
                    return "HotSauce";

                else return new_sauce;

            default:
                return new_sauce;

        }

    }

    private void doComboEffect()
    {
        combo_effect.GetComponent<ParticleSystem>().Play();
        dish_sauce.GetComponent<ShowCombo>().ShowComboPopup();
        new_combo_yay.PlayDelayed(0.5f);
        combo_poof.Play();
    }

    private Material GetComboMaterial(string existing_sauce, string new_sauce)
    {
        switch (existing_sauce)
        {
            case "HotSauce":
                if (new_sauce.Contains("HotSauce") || new_sauce.Contains("TargetSauce") || new_sauce.Contains("DigitalSauce"))
                    return hot;
                else
                    return sauce_material;

            case "XenoSauce":
                if (new_sauce.Contains("XenoSauce") || new_sauce.Contains("SciFiSauce") || new_sauce.Contains("MonsterSauce"))
                    return xeno;
                else return sauce_material;

            case "TronSauce":
                if (new_sauce.Contains("TronSauce") || new_sauce.Contains("SciFiSauce") || new_sauce.Contains("DigitalSauce"))
                    return tron;
                else return sauce_material;

            case "TrekSauce":
                if (new_sauce.Contains("TrekSauce") || new_sauce.Contains("TargetSauce") || new_sauce.Contains("ScifiSauce"))
                    return trek;
                else return sauce_material;

            case "MonsterSauce":
                if (new_sauce.Contains("MonsterSauce"))
                    return sauce_material;

                if (new_sauce.Contains("SciFiSauce"))
                {
                    doComboEffect();
                    return xeno;
                }

                if (new_sauce.Contains("TargetSauce"))
                {
                    return godzilla;
                }

                if (new_sauce.Contains("DigitalSauce"))
                {
                    return pokemon;
                }

                else return sauce_material;

            case "SciFiSauce":
                if (new_sauce.Contains("MonsterSauce"))
                {
                    doComboEffect();
                    return xeno;
                }

                if (new_sauce.Contains("SciFiSauce"))
                    return sauce_material;

                if (new_sauce.Contains("TargetSauce"))
                {
                    doComboEffect();
                    return trek;
                }

                if (new_sauce.Contains("DigitalSauce"))
                {
                    doComboEffect();
                    return tron;
                }

                else return sauce_material;

            case "DigitalSauce":
                if (new_sauce.Contains("MonsterSauce"))
                    return pokemon;

                if (new_sauce.Contains("SciFiSauce"))
                {
                    doComboEffect();
                    return tron;
                }

                if (new_sauce.Contains("TargetSauce"))
                {
                    doComboEffect();
                    return hot;
                }

                if (new_sauce.Contains("DigitalSauce"))
                    return sauce_material;

                else return sauce_material;

            case "TargetSauce":
                if (new_sauce.Contains("MonsterSauce"))
                    return godzilla;

                if (new_sauce.Contains("SciFiSauce"))
                {
                    doComboEffect();
                    return trek;
                }

                if (new_sauce.Contains("TargetSauce"))
                    return sauce_material;

                if (new_sauce.Contains("DigitalSauce"))
                {
                    doComboEffect();
                    return hot;
                }

                else return sauce_material;

            default:
                return sauce_material;

        }

    }

    private bool IsSameOrSub(string existing_sauce, string new_sauce)
    {
        switch (existing_sauce)
        {
            case "HotSauce":
                if (new_sauce.Contains("HotSauce") || new_sauce.Contains("TargetSauce") || new_sauce.Contains("DigitalSauce"))
                    return true;
                else return false;

            case "XenoSauce":
                if (new_sauce.Contains("XenoSauce") || new_sauce.Contains("SciFiSauce") || new_sauce.Contains("MonsterSauce"))
                    return true;
                else return false;

            case "TronSauce":
                if (new_sauce.Contains("TronSauce") || new_sauce.Contains("SciFiSauce") || new_sauce.Contains("DigitalSauce"))
                    return true;
                else return false;

            case "TrekSauce":
                if (new_sauce.Contains("TrekSauce") || new_sauce.Contains("TargetSauce") || new_sauce.Contains("SciFiSauce"))
                    return true;
                else return false;

            case "MonsterSauce":
                if (new_sauce.Contains("MonsterSauce"))
                    return true;
                else return false;

            case "SciFiSauce":
                if (new_sauce.Contains("SciFiSauce"))
                    return true;
                else return false;

            case "DigitalSauce":
                if (new_sauce.Contains("DigitalSauce"))
                    return true;
                else return false;

            case "TargetSauce":
                if (new_sauce.Contains("TargetSauce"))
                    return true;
                else return false;

            default:
                return false;

        }
    }

    private void SetDishSauce(GameObject dish_sauce)
    {
  
        if (!IsSameOrSub(dish_sauce.GetComponent<Saucable>().sauce_type, sauce_type))
        {
            string new_sauce_type = GetComboType(dish_sauce.GetComponent<Saucable>().sauce_type, sauce_type);
            Material new_sauce_material = GetComboMaterial(dish_sauce.GetComponent<Saucable>().sauce_type, sauce_type);

            dish_sauce.GetComponent<Saucable>().sauce_type = new_sauce_type;
            dish_sauce.GetComponent<SauceType>().sauce_type = new_sauce_type;
            dish_sauce.GetComponent<SauceType>().sauced_sushi_material = new_sauce_material;


            foreach(Transform child in dish_sauce.transform)
            {
                if (child.name.Contains("SpicyParticles") || child.name.Contains("TrekSauceParticles"))
                    Destroy(child.gameObject);
            }

            switch (new_sauce_type)
            {
                case "HotSauce":

                    GameObject spicy_particles = Instantiate(GameObject.Find("SpicyParticles"));
                    spicy_particles.transform.parent = dish_sauce.transform;
                    spicy_particles.transform.localPosition = new Vector3(0, 0.1f, 0);
                    spicy_particles.transform.localScale = new Vector3(1, 1, 1);
                    SauceObject(dish_sauce, new_sauce_material);
                    break;

                case "XenoSauce":
                    SauceObject(dish_sauce, new_sauce_material);
                    break;

                case "TronSauce":
                    SauceObject(dish_sauce, new_sauce_material);
                    break;

                case "TrekSauce":
                    GameObject trek_particles = Instantiate((GameObject)Resources.Load("TrekSauceParticles"), dish_sauce.transform.position, dish_sauce.transform.rotation, dish_sauce.transform);
                    trek_particles.transform.localPosition = new Vector3(0, -.01f, 0);
                    trek_particles.transform.localRotation = Quaternion.Euler(-90, 0, 0);
                    trek_particles.transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
                    Debug.Log(trek_particles + "was created");
                    SauceObject(dish_sauce, new_sauce_material);
                    break;

                case "MonsterSauce":
                    //call dart function
                    //TransformToDart(sushi);
                    SauceObject(dish_sauce, new_sauce_material);
                    break;

                case "SciFiSauce":
                    SauceObject(dish_sauce, new_sauce_material);
                    break;

                case "DigitalSauce":
                    SauceObject(dish_sauce, new_sauce_material);
                    break;

                case "TargetSauce":
                    SauceObject(dish_sauce, new_sauce_material);
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
