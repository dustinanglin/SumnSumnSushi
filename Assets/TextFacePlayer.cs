using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFacePlayer : MonoBehaviour
{
    private GameObject head;
    public bool facePlayer;

    // Start is called before the first frame update
    void Start()
    {
        head = GameObject.Find("CenterEyeAnchor");
    }

    // Update is called once per frame
    void Update()
    {
        if (facePlayer)
        {
            Vector3 lookAtHead = this.transform.position - head.transform.position; //reverse because Text is funky pants
            lookAtHead = Vector3.ProjectOnPlane(lookAtHead, Vector3.up); //project onto a "floor" plane so it only rotates around the Y axis
            this.transform.rotation = Quaternion.LookRotation(lookAtHead, Vector3.up);
        }
    }
}
