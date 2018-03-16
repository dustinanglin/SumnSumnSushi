using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorConvertObject : MonoBehaviour {

    public GameObject m_object;
    public Material m_material;

    public ColorConvertObject(GameObject curr_object, Material curr_material)
    {
        m_object = curr_object;
        m_material = curr_material;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
