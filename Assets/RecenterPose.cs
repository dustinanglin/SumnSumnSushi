using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecenterPose : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) || OVRInput.Get(OVRInput.Button.PrimaryThumbstick,OVRInput.Controller.Active))
        {
            OVRManager.display.RecenterPose();
        }
    }
}
