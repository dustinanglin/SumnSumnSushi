using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VignetteControl : MonoBehaviour
{
    public GameObject Vignette, CameraBody, Camera;
    private MeshRenderer[] meshRenderers;
    private bool doVignette;
    // Start is called before the first frame update
    void Start()
    {
        doVignette = false;
        Vignette.SetActive(false);
        Camera.SetActive(true);
        
        meshRenderers = CameraBody.GetComponentsInChildren<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vignette.SetActive(doVignette);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger enter:" + other.name);

        if (other.name.Contains("ViewFinder"))
        {
            doVignette = true;
            foreach (MeshRenderer mesh in meshRenderers)
                mesh.enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger exit:" + other.name);
        if (other.name.Contains("ViewFinder"))
        {
            doVignette = false;
            foreach (MeshRenderer mesh in meshRenderers)
                mesh.enabled = true;
        }
    }
}
