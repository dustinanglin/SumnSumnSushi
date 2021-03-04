using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackOut : MonoBehaviour
{
    public GameObject directionalLight;
    public GameObject pointLight;
    public Material blackout_sky;
    public Material default_sky;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void turn_lights_out()
    {
        pointLight.SetActive(true);
        directionalLight.SetActive(false);
        RenderSettings.skybox = blackout_sky;
        RenderSettings.ambientLight = Color.black;
        RenderSettings.reflectionIntensity = 0;
    }

    public void turn_lights_on()
    {
        pointLight.SetActive(false);
        directionalLight.SetActive(true);
        RenderSettings.skybox = default_sky;
        RenderSettings.ambientLight = Color.white;
        RenderSettings.reflectionIntensity = 1;
    }

}
