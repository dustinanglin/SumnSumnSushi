using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseMask : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Renderer rend in GetComponentsInChildren<Renderer>())
        {
            rend.material.renderQueue = 2002;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
