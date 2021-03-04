using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawRayToSun : MonoBehaviour
{
    private Vector3 lightVector;
    public GameObject directionalLight;
    public GameObject trackingSphere;
    public GameObject playerHead;
    public GameObject grabSpot;
    private bool popped;
    public float distance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        popped = false;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = directionalLight.transform.rotation;
        //Debug.DrawRay(this.transform.position, -1 * this.transform.forward, Color.red);
        //Debug.DrawRay(trackingSphere.transform.position, trackingSphere.transform.forward * -1, Color.blue);
        if (trackingSphere.GetComponent<Grabbable>().isGrabbed == false)
        {
            trackingSphere.transform.localPosition = new Vector3(0, 0, distance * -1);
            trackingSphere.transform.rotation = this.transform.rotation;
            trackingSphere.GetComponent<BlackOut>().turn_lights_on();
            trackingSphere.GetComponent<MeshRenderer>().enabled = false;
            trackingSphere.transform.Find("SunGlow").gameObject.SetActive(false);
            popped = false;
        }
        else
        {
            trackingSphere.transform.rotation = Quaternion.LookRotation(playerHead.transform.position - trackingSphere.transform.position);
            trackingSphere.GetComponent<BlackOut>().turn_lights_out();
            trackingSphere.GetComponent<MeshRenderer>().enabled = true;
            trackingSphere.transform.Find("SunGlow").gameObject.SetActive(true);
            if (!popped)
            {
                trackingSphere.transform.position = grabSpot.transform.position;
                popped = true;
            }
        }
    }
}
