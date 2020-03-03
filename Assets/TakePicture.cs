using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePicture : MonoBehaviour
{
    private bool screenshot;
    public GameObject Photospot;
    private Camera Camera;
    //public GameObject CameraCanvas;
    //private Renderer CanvasRenderer;
    // Start is called before the first frame update
    void Start()
    {
        screenshot = false;
        Camera = GetComponent<Camera>();
        //CanvasRenderer = CameraCanvas.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Debug.Log("do screenshot!" + Time.fixedTime);
            screenshot = true;
        }
    }

    private void OnPostRender()
    {
        //Debug.Log("In Post Render");
        if (screenshot)
        {
            /*RenderTexture temp = new RenderTexture(1500, 1000,24,RenderTextureFormat.ARGB32);
            Camera.targetTexture = temp;
            RenderTexture.active = temp;*/
            Debug.Log("Picture taken");
            screenshot = false;
            RenderTexture.active = this.GetComponent<Camera>().targetTexture;
            RenderTexture temp = RenderTexture.active;
            Texture2D texture = new Texture2D(temp.width, temp.height, TextureFormat.RGB24, false);
            //Read the pixels in the Rect starting at 0,0 and ending at the screen's width and height
            texture.ReadPixels(new Rect(0, 0, temp.width, temp.height), 0, 0, false);
            texture.Apply();

            //RenderTexture.active = null;
            //Camera.targetTexture = null;

            Photospot.GetComponent<Renderer>().material.mainTexture = texture;

            //Graphics.Blit()
        }
    }
}
