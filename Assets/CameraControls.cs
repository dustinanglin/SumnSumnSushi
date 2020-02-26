using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class CameraControls : MonoBehaviour
{
    private OVRInput.Controller ControllingHand;
    private RenderTexture renderTexture;
    public AnimationCurve focalsize, aperture, focallength, focusrange;
    public GameObject Hand, SceneCamera, DSLRCameraPreview, Photospot;
    private Transform Cube, ShutterMask;
    private float zoom, focus, initial_amount, l_z, l_f, screenshot_time_local, mask_lerp;
    private Vector3 initial;
    private bool zooming, focusing, zoomreset, focusreset, screenshot, screenshotreset;
    public float zoom_speed, focus_speed, screenshot_time;
    private Camera Camera, CameraPreview;
    private DepthOfField DepthOfField, DOFPreview;

    // Start is called before the first frame update
    void Start()
    {
        Camera = SceneCamera.GetComponent<Camera>();
        DepthOfField = SceneCamera.GetComponent<DepthOfField>();

        renderTexture = Camera.targetTexture;

        CameraPreview = DSLRCameraPreview.GetComponent<Camera>();
        DOFPreview = DSLRCameraPreview.GetComponent<DepthOfField>();

        zoom = Camera.fieldOfView;
        focus = 0;

        DepthOfField.focalSize = focalsize.Evaluate(zoom / 90);
        DepthOfField.aperture = aperture.Evaluate(zoom / 90);
        DepthOfField.focalLength = focallength.Evaluate(zoom / 90) + focus;

        Debug.Log("Focal Length: " + DepthOfField.focalLength);

        ControllingHand = OVRInput.Controller.LTouch;

        zooming = focusing = screenshot = false;
        initial_amount = 0;
        zoomreset = true;
        focusreset = true;
        screenshotreset = true;

        Cube = transform.Find("DirectionCube");
        ShutterMask = transform.Find("ShutterMask");

        mask_lerp = 0;

    }

    // Update is called once per frame
    void Update()
    {
        GetInputs();

        if (zooming)
            DoZoom();
        else
            zoomreset = true;

        if (focusing)
            DoFocus();
        else
            focusreset = true;

        MatchDSLR();

        if (screenshot)
            DoPictureEffect();
        else
            screenshotreset = true;


        transform.localRotation = Quaternion.Euler(0, 0, DSLRCameraPreview.transform.eulerAngles.z);
    }


    private void DoPictureEffect()
    {
        if (screenshotreset)
        {
            screenshotreset = false;
            screenshot_time_local = screenshot_time;
            initial = ShutterMask.localScale;
            mask_lerp = 0;
        }

        if (screenshot_time_local >= 0)
        {
            if (screenshot_time_local > screenshot_time / 2.0f)
            {
                ShutterMask.localScale = Vector3.Lerp(initial, new Vector3(0, 0, 0), mask_lerp);
                mask_lerp += Time.deltaTime * 1 / (screenshot_time / 2.0f);
                Debug.Log(mask_lerp);
                //ShutterMask.localScale = new Vector3(ShutterMask.localScale.x - (Time.deltaTime * 10) / (screenshot_time / 2), ShutterMask.localScale.y - (Time.deltaTime * 10) / (screenshot_time / 2), ShutterMask.localScale.z);
            }
            else
            {
                ShutterMask.localScale = Vector3.Lerp(new Vector3(0, 0, 0), initial, 1 - mask_lerp);
                mask_lerp -= Time.deltaTime * 1 / (screenshot_time / 2.0f);
                Debug.Log(mask_lerp);
                //ShutterMask.localScale = new Vector3(ShutterMask.localScale.x + (Time.deltaTime * 10) / (screenshot_time / 2), ShutterMask.localScale.y + (Time.deltaTime * 10) / (screenshot_time / 2), ShutterMask.localScale.z);
            }

            screenshot_time_local -= Time.deltaTime;
        }
        else
        {
            ShutterMask.localScale = initial;
            screenshot = false;
        }

    }

    private void MatchDSLR()
    {
        CameraPreview.fieldOfView = Camera.fieldOfView;
        DOFPreview.focalLength = DepthOfField.focalLength;
        DOFPreview.focalSize = DepthOfField.focalSize;
        DOFPreview.aperture = DepthOfField.aperture;
    }


    private void DoZoom()
    {
        if (zoomreset)
        {
            Cube.up = Vector3.ProjectOnPlane(Hand.transform.right, transform.forward);
            zoomreset = false;
            initial_amount = zoom;
        }
        Debug.DrawRay(Cube.position, Cube.up, Color.green);
        l_z = Vector3.SignedAngle(Cube.up, Vector3.ProjectOnPlane(Hand.transform.right, transform.forward), transform.forward);
        zoom = Mathf.Clamp(initial_amount + l_z * zoom_speed,5,90);
        Camera.fieldOfView = zoom;
        var normalized_zoom = zoom / 90;
        DepthOfField.focalSize = focalsize.Evaluate(normalized_zoom);
        DepthOfField.aperture = aperture.Evaluate(normalized_zoom);
        DepthOfField.focalLength = focallength.Evaluate(normalized_zoom) + focus;
        Debug.Log(Vector3.SignedAngle(Cube.up, Vector3.ProjectOnPlane(Hand.transform.right, transform.forward), transform.forward));  
    }

    private void DoFocus()
    {
        if (focusreset)
        {
            Cube.up = Vector3.ProjectOnPlane(Hand.transform.right, transform.forward);
            focusreset = false;
            initial_amount = focus;
        }
        var normalized_zoom = zoom / 90;
        Debug.DrawRay(Cube.position, Cube.up, Color.green);
        l_f = Vector3.SignedAngle(Cube.up, Vector3.ProjectOnPlane(Hand.transform.right, transform.forward), transform.forward);
        focus = Mathf.Clamp(initial_amount + -1 * l_f * focus_speed,-10,10);
        Debug.Log("Focus: " + focus);
        DepthOfField.focalLength = focallength.Evaluate(normalized_zoom) + focus;
        //Debug.Log(Vector3.SignedAngle(Cube.up, Vector3.ProjectOnPlane(Hand.transform.right, transform.forward), transform.forward));
    }

    private void GetInputs()
    {
        zooming = (OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger, ControllingHand) > .2f);
        focusing = (OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, ControllingHand) > .2f);
        if (OVRInput.GetDown(OVRInput.Button.One))
            screenshot = true;
        //initial_rot = Hand.transform.localEulerAngles.x;
        Debug.DrawRay(Hand.transform.position, Hand.transform.right, Color.blue);
        Debug.DrawRay(transform.position, Vector3.ProjectOnPlane(Hand.transform.right, transform.forward), Color.cyan);
        Debug.DrawRay(transform.position, transform.right, Color.magenta);
    }
}
