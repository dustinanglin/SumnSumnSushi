﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmNomNom : MonoBehaviour {

    private GameObject colliding_object;
    private GameObject omNomText;
    private GameObject om;
    private GameObject nom1;
    private GameObject nom2;
    private bool showing_nom = false;
    public float countdown_time = 10;
    private float timer = 0;

    // Use this for initialization
    void Start () {
        omNomText = GameObject.Find("OmNomText");
        om = omNomText.transform.Find("Om").gameObject;
        nom1 = omNomText.transform.Find("Nom1").gameObject;
        nom2 = omNomText.transform.Find("Nom2").gameObject;
        //Debug.Log(om.name + nom1.name + nom2.name);
     }
	
	// Update is called once per frame
	void Update () {
       
		if (showing_nom)
        {
            
            timer -= Time.unscaledDeltaTime;
            if (timer <= countdown_time && !om.activeSelf)
            {
                Debug.Log(timer);
                Debug.Log("Showing Om");
                om.SetActive(true);
            }
            if (timer <= (countdown_time * 8 / 10) && !nom1.activeSelf)
            {
                Debug.Log(timer);
                Debug.Log("Showing Nom1");
                nom1.SetActive(true);
            }
            if (timer <= (countdown_time * 6 / 10) && !nom2.activeSelf)
            {
                Debug.Log(timer);
                Debug.Log("Showing Nom1");
                nom2.SetActive(true);
            }
            if (timer <= 0)
            {
                showing_nom = false;
                om.SetActive(false);
                nom1.SetActive(false);
                nom2.SetActive(false);
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent != null)
            colliding_object = other.gameObject.transform.parent.gameObject;
        else
            colliding_object = other.gameObject;

        
        if (colliding_object.GetComponent<Grabbable>() != null)
        {
            string ishot = colliding_object.tag;
            Destroy(colliding_object);
            Debug.Log("Fish ate!");

            if (colliding_object.GetComponent<Saucable>())
            {
                string sauce_type = colliding_object.GetComponent<Saucable>().sauce_type;
                switch (sauce_type)
                {
                    case "HotSauce":
                    GameObject.Find("SceneDirector").GetComponent<SceneDirector>().SetHot();
                    break;
                }
            }
            else if (!showing_nom)
                ShowOmNom();
        }
    }

    private void ShowOmNom()
    {
        showing_nom = true;
        timer = countdown_time;
    }
}
