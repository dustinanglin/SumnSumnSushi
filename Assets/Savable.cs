using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savable : MonoBehaviour
{
    public string type;
    private SaveandLoad saver;

    // Start is called before the first frame update
    void Start()
    {
        type = this.name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnDestroy()
    //{
    //    if (saver)
    //        saver.RemoveSushi(this.gameObject);
    //}
}
