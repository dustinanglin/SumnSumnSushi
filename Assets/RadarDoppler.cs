using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarDoppler : MonoBehaviour {

    private AudioSource target_sound;
    public float loop_delay, ping_delay = 1.5f;
    public float min_warp, max_warp, tweek;
    private float local_time = 0f;
    private float pitch_adjust = 0f;
    private bool alien_exists = false;

	// Use this for initialization
	void Start () {
        target_sound = GetComponents<AudioSource>()[1];
	}
	
	// Update is called once per frame
	void Update () {
        alien_exists = ping_delay <= 0f;
        ping_delay -= Time.deltaTime;

        if (GameObject.Find("XenomorphSpaceballsReAnimate"))
        {
            pitch_adjust = 1 + (1 / Vector3.Distance(GameObject.Find("XenomorphSpaceballsReAnimate").transform.position, transform.position) / tweek);
            //Debug.Log(pitch_adjust);
        }

        if (local_time >= loop_delay)
        {
            target_sound.pitch = Mathf.Clamp(pitch_adjust, min_warp, max_warp);
            if (alien_exists)
            {
                target_sound.Play();
                Debug.Log("Sound Played!");
            }
            local_time = 0;
        }
        local_time += Time.deltaTime;
	}
}
