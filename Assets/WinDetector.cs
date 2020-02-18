using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WinDetector : MonoBehaviour {

    private int deathcount = 0;
    private int enemynum = 0;
    public float text_time;
    public float total_time = 6;
    public float fade_timer = 1;
    private float ticker;
    private float fade = 0;
    private GameObject sushi, hot, txtbkg, endgame;
    private AudioSource end_sound;
    private bool increment, not_played;

	// Use this for initialization
	void Start () {
        enemynum = GameObject.FindGameObjectsWithTag("Enemy").Length;
        endgame = GameObject.Find("EndGame");
        end_sound = GetComponent<AudioSource>();
        sushi = endgame.transform.Find("Super").gameObject;
        hot = endgame.transform.Find("Hot").gameObject;
        txtbkg = endgame.transform.Find("TextBkg").gameObject;
        ticker = text_time;
        increment = false;
        not_played = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (deathcount == enemynum)
        {
            txtbkg.SetActive(true);
            EndGame();
            if (not_played)
            {
                foreach (AudioSource sound in FindObjectsOfType<AudioSource>())
                {
                    sound.volume = .1f;
                }
                end_sound.volume = 1f;
                end_sound.Play();
                not_played = false;
            }
        }
	}

    private void EndGame()
    {
        if (total_time <= 0)
        {
            SceneManager.LoadScene("SushiHub");
        }

        if (ticker < (-1 * text_time))
            increment = true;

        if (ticker > text_time)
            increment = false;

        if (!increment)
        {
            sushi.SetActive(true);
            hot.SetActive(false);
            ticker -= Time.deltaTime;
        }
        else 
        {
            sushi.SetActive(false);
            hot.SetActive(true);
            ticker += Time.deltaTime;
        }

        if (fade_timer >= 0)
        {
            txtbkg.GetComponent<Renderer>().material.color = new Color(.162f, .162f, .162f, fade);
            if (fade <= .7f)
                fade += Time.deltaTime;
            fade_timer -= Time.deltaTime;
            Mathf.Clamp(fade, 0, .7f);
            //Debug.Log(fade);
        }

        total_time -= Time.deltaTime;
       
        

    }

    public void increment_death()
    {
        deathcount++;
    }
}
