using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiatePhaserGame : MonoBehaviour {

    private GenerateSpheres director;
    private PhaserGame game_manager;
    public GameObject director_container, gm_object;
    private AudioSource[] button_audio;
    public float delay_time;
    private float delay_time_local;
    private bool audio_played, initiate_game = false;


	// Use this for initialization
	void Start () {
        director = director_container.GetComponent<GenerateSpheres>();
        game_manager = gm_object.GetComponent<PhaserGame>();
        delay_time_local = delay_time;
        button_audio = GetComponents<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (initiate_game)
        {
            if (delay_time_local >= 0)
            {
                delay_time_local -= Time.deltaTime;
                //Debug.Log(delay_time_local);
            }
            else if (!director.isInitiated())
            {
                Debug.Log("initiate game!");
                game_manager.setInitiated(true);
                director.setInitiate();
                button_audio[0].Play();
            }

            if (!audio_played)
            {
                button_audio[1].Play();
                audio_played = true;
            }
        }
    }

    public void resetGame()
    {
        initiate_game = false;
        delay_time_local = delay_time;
        audio_played = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.name.Contains("Pusher") && !director.isInitiated() && !initiate_game)
        {
            initiate_game = true;
        }

        //Debug.Log(other.name);
    }
}
