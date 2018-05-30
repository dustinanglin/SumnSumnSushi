using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TronBladeGlow : MonoBehaviour {

    public float glow_speed, glow_acceleration = 1f;
    public float m_speed = 1f;
    private float m_time = 1f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        if (m_speed < glow_speed)
        {
            
            Shader.SetGlobalFloat("_glow_speed", m_speed);
            m_speed += Time.deltaTime * glow_acceleration;
        }
    }
}
