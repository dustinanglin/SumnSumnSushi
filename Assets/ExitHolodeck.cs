using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitHolodeck : MonoBehaviour {

    public GameObject button, manager, transport_cube;
    public float end_delay = 0f;
    private InitiatePhaserGame initiate_button;
    private TransporterFade transport_effect;
    private PhaserGame game_manager;
    private AudioSource end_transport, end_button;
    private bool loadchopstick = false;
    private TrackAchievements tracker;

	// Use this for initialization
	void Start () {
        initiate_button = button.GetComponent<InitiatePhaserGame>();
        game_manager = manager.GetComponent<PhaserGame>();
        end_transport = GetComponents<AudioSource>()[0];
        end_button = GetComponents<AudioSource>()[1];
        transport_effect = transport_cube.GetComponent<TransporterFade>();
        StartCoroutine(LoadLevel());

        tracker = new TrackAchievements();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator LoadLevel()
    {
        var async = SceneManager.LoadSceneAsync("SushiHub");
        async.allowSceneActivation = false;

        while (async.progress < .9f)
        {
            Debug.Log(async.progress);
            yield return null;
        }
        Debug.Log(async.progress);

        while (!loadchopstick)
            yield return null;

        yield return new WaitForSeconds(end_delay);

        async.allowSceneActivation = true;

    }


    private void OnTriggerEnter(Collider other)
    {

        if (other.name.Contains("Pusher") && !initiate_button.initiate_game && !game_manager.showEnd)
        {
            //SceneManager.LoadScene("Chopstick Test", LoadSceneMode.Single);
            end_transport.Play();
            end_button.Play();
            loadchopstick = true;
            transport_effect.transport_out = true;
            tracker.SaveLastLevel("holodeck");
        }

        //Debug.Log(other.name);
    }
}
