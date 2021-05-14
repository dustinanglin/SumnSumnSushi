using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishReanimator : MonoBehaviour
{
    private SaveandLoad saver;
    private bool appquit = false;

    [SerializeField]
    private GameObject dishspot;

    private GameObject dishtemplate;

    // Start is called before the first frame update
    void Start()
    {
        saver = GameObject.Find("SceneDirector").GetComponent<SaveandLoad>();
        dishtemplate = GameObject.Find("SauceDishTemplate");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnApplicationQuit()
    {
        appquit = true;
    }

    private void OnDestroy()
    {
        if (!appquit)
        {
            GameObject newDish = Instantiate(dishtemplate, dishspot.transform.position, dishspot.transform.rotation);
            newDish.name = "SauceDish";
            Debug.Log("Dish destroyed and reanimated");
        }
    }
}
