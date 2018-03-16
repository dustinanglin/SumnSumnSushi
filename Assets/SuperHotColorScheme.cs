using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SuperHotColorScheme : MonoBehaviour {


    private List<ColorConvertObject> worldObjects = new List<ColorConvertObject>();
    private Material whitematerial, skybox;
    private GameObject Light1, Light2, DirLight;
    private bool lerping = false;
    private Scene nextScene;

	// Use this for initialization
	void Start () {

        Light1 = GameObject.Find("Lamp1Light");
        Light2 = GameObject.Find("Lamp2Light");
        DirLight = GameObject.Find("Directional Light");
        whitematerial = Resources.Load("SuperWhite", typeof(Material)) as Material;
        skybox = Resources.Load("SuperHotLightBox", typeof(Material)) as Material;
        //nextScene = SceneManager.GetSceneByName("_scenes/OtherSceneTest");
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (GameObject item in FindObjectsOfType(typeof(GameObject)) as GameObject[])
            {
                if (item.GetComponent<Renderer>())
                    if (item.GetComponent<Renderer>().name != "No Name")
                        worldObjects.Add(new ColorConvertObject(item, item.GetComponent<Renderer>().material));
            }
            Debug.Log("List created");
            lerping = true;
            Light1.GetComponent<Light>().color = Color.white;
            Light2.GetComponent<Light>().color = Color.white;
            DirLight.GetComponent<Light>().color = Color.white;
            RenderSettings.ambientLight = new Color(.62f, .62f, .62f);
            RenderSettings.skybox = skybox;
            DynamicGI.UpdateEnvironment();
        }

        if (lerping)
        {
            foreach (ColorConvertObject item in worldObjects)
            {
                if (item.m_object.GetComponent<Renderer>())
                    item.m_object.GetComponent<Renderer>().material.Lerp(item.m_material, whitematerial, Time.deltaTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            if (worldObjects != null)
            {
                foreach (ColorConvertObject item in worldObjects)
                {
                    if (item.m_object.GetComponent<Renderer>())
                        item.m_object.GetComponent<Renderer>().material = item.m_material;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            SceneManager.LoadScene("_scenes/OtherSceneTest", LoadSceneMode.Single);
            Debug.Log("SceneLoad!");
        }
	}
}
