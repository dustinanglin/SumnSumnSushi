using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaserGame : MonoBehaviour {

    private bool gameInitiated = false;
    public GameObject generator_obj, initiator_obj;
    private GenerateSpheres generator;
    private InitiatePhaserGame initiator;
    public int shots_allowed, fails_allowed, total_spheres, end_delay;
    private int current_shots, current_misses, current_successes;

    //private AudioClip fail, success;

	// Use this for initialization
	void Start () {
        current_shots = 0;
        current_misses = 0;
        generator = generator_obj.GetComponent<GenerateSpheres>();
        initiator = initiator_obj.GetComponent<InitiatePhaserGame>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameInitiated)
        {
            checkSuccess();
        }
	}

    private void checkSuccess()
    {
        if ((current_shots >= shots_allowed) || (current_successes >= total_spheres) || (current_misses >= fails_allowed))
        {
            Debug.Log("Game Finished!");
            this.GetComponents<AudioSource>()[2].Play(22050 * (ulong)end_delay);
            gameInitiated = false;
            resetGame();
        }
    }

    public void playFail()
    {

        if (gameInitiated)
        {
            this.GetComponents<AudioSource>()[0].Play();
            current_shots++;
            current_misses++;
        }



    }

    public void playSuccess()
    {
        if (gameInitiated)
        {
            this.GetComponents<AudioSource>()[1].Play();
            current_shots++;
            current_successes++;
        }


    }

    public void setInitiated(bool val)
    {
        gameInitiated = val;
    }

    private void resetGame()
    {
        current_shots = 0;
        current_misses = 0;
        current_successes = 0;
        generator.endGame();
        initiator.resetGame();
    }


}
