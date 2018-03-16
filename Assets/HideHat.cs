using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideHat : MonoBehaviour {

    AnimationState myClip;
    GameObject hat1, hat2;
    GameObject alien;
    Light spotlight, spotlight2;
    ReflectionProbe rp;

    public float hat_time = 0f;
    public float fade_start = 0f;
    private bool doFade, doWalk, doRun = false;
    public Color finalColor;
    private Color sceneColor;
    private float fade_time = 0f;
    public float fade_in_length = 1f;
    private float curr_spot_angle = 0f;
    public float final_spot_angle = 0f;
    public float dance_speed = 0f;
    public float dance_start = 0f;
    public float run_start = 0f;
    public float run_speed = 0f;
    private float dance = 0f;
    public float probe_start_value = 0f;
   

	// Use this for initialization
	void Start () {
        hat1 = GameObject.Find("Hat");
        hat2 = GameObject.Find("Hat_2");
        hat2.SetActive(false);

        spotlight = GameObject.Find("AlienSpot").GetComponent<Light>();
        curr_spot_angle = spotlight.spotAngle;

        spotlight2 = GameObject.Find("AlienSpot2").GetComponent<Light>();
        spotlight2.intensity = 0;

        rp = GameObject.Find("ReflectionProbe1").GetComponent<ReflectionProbe>();
        rp.intensity = probe_start_value;

        alien = GameObject.Find("AlienContainer");

        sceneColor = RenderSettings.ambientLight;

        Animation myAnim = GetComponent<Animation>();
        myClip = myAnim["Dance"];
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(myClip.time);
        if (myClip.time >= hat_time)
        {
            hat1.SetActive(false);
            hat2.SetActive(true);
        }

        if (myClip.time >= fade_start)
        {
            doFade = true;
        }

        if (myClip.time >= dance_start)
        {
            doWalk = true;
        }

        if (myClip.time >= run_start)
        {
            doRun = true;
        }

        if (doFade)
        {
            RenderSettings.ambientLight = Color.Lerp(sceneColor, finalColor, fade_time);
            rp.intensity = Mathf.Lerp(probe_start_value, 1, fade_time);
            //spotlight2.intensity = Mathf.Lerp(0, 1, fade_time);
            if (fade_in_length != 0)
                fade_time += (Time.deltaTime / fade_in_length);

            spotlight.spotAngle = Mathf.Lerp(curr_spot_angle, final_spot_angle, fade_time);

            if (!doRun)
            {
                spotlight.transform.LookAt(hat2.transform);
                spotlight2.transform.LookAt(hat2.transform);
            }
        }

        if (doWalk)
        {
            dance = Time.deltaTime * dance_speed;
            if (doRun)
                dance = dance * run_speed;
            alien.transform.position = new Vector3(alien.transform.position.x + dance, alien.transform.position.y, alien.transform.position.z);
        }

        if (doRun)
        {
        }

	}
}
