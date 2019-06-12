using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaserGame : MonoBehaviour {

    private bool gameInitiated, win, endSoundPlayed = false;
    public bool showEnd = false;
    public GameObject generator_obj, initiator_obj, lcars_screen;
    private GenerateSpheres generator;
    private InitiatePhaserGame initiator;
    public Material lcars1;
    private TextMesh shots, hits, misses, success1, success2, failure1, failure2, accuracy, rank;
    public int shots_allowed, fails_allowed, total_spheres, end_delay, message_time;
    private float end_message_time;
    private int current_shots, current_misses, current_successes;
    private float last_accuracy;

    //private AudioClip fail, success;

	// Use this for initialization
	void Start () {
        current_shots = 0;
        current_misses = 0;
        end_message_time = message_time;
        generator = generator_obj.GetComponent<GenerateSpheres>();
        initiator = initiator_obj.GetComponent<InitiatePhaserGame>();
        shots = GameObject.Find("ScoreTextShots").GetComponent<TextMesh>();
        hits = GameObject.Find("ScoreTextHits").GetComponent<TextMesh>();
        misses = GameObject.Find("ScoreTextMisses").GetComponent<TextMesh>();
        success1 = GameObject.Find("WinText").GetComponent<TextMesh>();
        success2 = GameObject.Find("WinText2").GetComponent<TextMesh>();
        failure1 = GameObject.Find("FailText").GetComponent<TextMesh>();
        failure2 = GameObject.Find("FailText2").GetComponent<TextMesh>();
        accuracy = GameObject.Find("Accuracy").GetComponent<TextMesh>();
        rank = GameObject.Find("Rank").GetComponent<TextMesh>();
       }
	
	// Update is called once per frame
	void Update () {
		if (gameInitiated)
        {
            shots.text = "SHOTS REMAINING " + (shots_allowed - current_shots);
            hits.text = "HITS " + current_successes;
            misses.text = "MISSES " + current_misses;
            checkSuccess();
        }
        if (showEnd && end_message_time > 0f)
        {
            end_message_time -= Time.deltaTime;

            if (win)
            {
                success1.text = "SUCCESS";
                success2.text = "RANGE SEQUENCE COMPLETE";
                if (!endSoundPlayed)
                {
                    endSoundPlayed = true;
                    this.GetComponents<AudioSource>()[3].Play();
                }
            }
            else
            {
                failure1.text = "FAILURE";
                failure2.text = "RETRY RANGE SEQUENCE";
                if (!endSoundPlayed)
                {
                    endSoundPlayed = true;
                    this.GetComponents<AudioSource>()[4].Play();
                }
            }

            accuracy.text = "ACCURACY | " + last_accuracy + "%";
            rank.text = "RANK | " + returnRank(last_accuracy);

            if (end_message_time <= 0f)
            {
                showEnd = false;
                endSoundPlayed = false;
                end_message_time = message_time;
                success1.text = "";
                success2.text = "";
                failure1.text = "";
                failure2.text = "";
                accuracy.text = "";
                rank.text = "";
                lcars_screen.GetComponent<Renderer>().material = lcars1;
            }
        }

	}

    private void checkSuccess()
    {
        if ((current_shots >= shots_allowed) || (current_successes >= total_spheres) || (current_misses >= fails_allowed))
        {
            Debug.Log("Game Finished!");
            win = current_misses <= 10;
            this.GetComponents<AudioSource>()[2].Play(22050 * (ulong)end_delay);
            gameInitiated = false;
            resetGame();
        }
    }

    private string returnRank(float accuracy)
    {
        if (accuracy == 100)
            return "GUINAN";
        else if (accuracy >= 90)
            return "PICARD";
        else if (accuracy >= 75)
            return "RIKER";
        else if (accuracy >= 60)
            return "WORF";
        else if (accuracy > 50)
            return "GEORDI";
        else if (accuracy == 50)
            return "WESLEY";
        else if (accuracy >= 25)
            return "BARKLEY";
        else if (accuracy >= 5)
            return "REDSHIRT";
        else
            return "SPOT";

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
        if (current_shots != 0)
            last_accuracy = Mathf.Round((current_successes * 1f) / (current_shots * 1f) * 100f);
        showEnd = true;
        current_shots = 0;
        current_misses = 0;
        current_successes = 0;
        shots.text = "";
        hits.text = "";
        misses.text = "";
        generator.endGame();
        initiator.resetGame();
    }


}
