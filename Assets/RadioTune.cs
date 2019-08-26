using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioTune : MonoBehaviour {

    public bool tune_new_audio = false;
    private int max_tunes, current_track, current_static;
    private bool start_static, changing = false;
    private AudioSource[] audioSources;

	// Use this for initialization
	void Start () {
        audioSources = GetComponents<AudioSource>();
        max_tunes = GetComponents<AudioSource>().Length;
        current_track = Random.Range(3, max_tunes);
        //Debug.Log("current track:" + current_track);
        audioSources[current_track].Play();
	}
	
	// Update is called once per frame
	void Update () {
		if (tune_new_audio)
        {
            changing = true;
            start_static = true;
            tune_new_audio = false;
        }

        if (changing)
        {
            ChangeTrack();
        }
	}

    private void ChangeTrack()
    {

        if (start_static)
        {
            audioSources[current_track].Stop();
            current_static = Random.Range(0, 2);
            start_static = false;
            audioSources[current_static].Play();
        }
        if (!start_static && !audioSources[current_static].isPlaying)
        {
            current_track = Random.Range(3, max_tunes);
            audioSources[current_track].Play();
            audioSources[current_track].time = Random.Range(0f, audioSources[current_track].clip.length / 2);
            changing = false;
        }
    }
}
