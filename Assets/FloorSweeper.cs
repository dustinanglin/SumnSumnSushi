using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorSweeper : MonoBehaviour
{
    private SaveandLoad saver;

    [SerializeField]
    private float cleanUptime;

    // Start is called before the first frame update
    void Start()
    {
        saver = GameObject.Find("SceneDirector").GetComponent<SaveandLoad>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject go = other.gameObject;

        if (go.GetComponent<Savable>())
        {
            if (go.name.Contains("Sauce"))
                saver.RemoveSauce(go);
            else
                saver.RemoveSushi(go);
        }

        if (go.transform.parent)
            if (go.transform.parent.gameObject.GetComponent<Savable>())
            {
                if (go.name.Contains("Sauce"))
                    saver.RemoveSauce(go.transform.parent.gameObject);
                else
                    saver.RemoveSushi(go.transform.parent.gameObject);
            }

        if (go.GetComponent<Grabbable>())
            Destroy(go, cleanUptime);
        else if (go.transform.parent)
            if (go.transform.parent.gameObject.GetComponent<Grabbable>())
                Destroy(go.transform.parent.gameObject, cleanUptime);
    }
}
