using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SauceType : MonoBehaviour {

    private ParticleSystem sauce;
    private ParticleSystemRenderer sauce_rend;
    public Material sauce_material;
    public Material bottle_material;
    public Material cap_color;
    public Material sauced_sushi_material;
    public string sauce_type;

    private GameObject cap, bottle_sauce;


	// Use this for initialization
	void Start () {
        cap = transform.Find("SauceCap").gameObject;
        bottle_sauce = transform.Find("SauceBottle_1").gameObject;
        sauce = transform.Find("PourSpot/SauceParticles").GetComponent<ParticleSystem>();
        sauce_rend = transform.Find("PourSpot/SauceParticles").GetComponent<ParticleSystemRenderer>();

        if (bottle_material != null)
            bottle_sauce.GetComponent<Renderer>().material = bottle_material;

        if (sauce_material != null)
            sauce_rend.material = sauce_material;

        if (cap_color != null)
            cap.GetComponent<Renderer>().material = cap_color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
