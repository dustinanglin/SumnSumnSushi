using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlienEvents : MonoBehaviour {

    private Animation[] sushilogo, lamp1, lamp2, chestburster;
    private TrackAchievements tracker;
    private AudioSource spark, buzz, explode, burst;
    public float light_flicker_delay, alien_burst_delay, alien_dance_delay, tracker_delay, end_of_scene = 0f;
    public float light_flicker_times = 1f;

    private ParticleSystem[] particles;
    public float spark_delay = 0f;
    public bool do_not_end = false;

    private GameObject burst_spot, dance_alien, spot_light, door, tracker_dot;
    private HideHat dance_start;


    public float time = 0f;
    private float spark_sound_delay = 0f;
    private bool play_spark_sound = true;
    private bool play_explode = true;
    private bool play_burst = true;

    private bool loadchopstick = false;

	// Use this for initialization
	void Start () {
        sushilogo = GameObject.Find("LogoText").GetComponentsInChildren<Animation>();
        lamp1 = GameObject.Find("Lantern1").GetComponentsInChildren<Animation>();
        lamp2 = GameObject.Find("Lantern2").GetComponentsInChildren<Animation>();
        particles = GameObject.Find("Sparks").GetComponentsInChildren<ParticleSystem>();
        chestburster = GameObject.Find("BurstSpot").GetComponentsInChildren<Animation>();

        burst_spot = GameObject.Find("BurstSpot");
        burst_spot.SetActive(false);

        dance_alien = GameObject.Find("XenomorphSpaceballsReAnimate");
        dance_alien.SetActive(false);

        spot_light = GameObject.Find("AlienSpot");
        spot_light.SetActive(false);

        door = GameObject.Find("DoorContainer");
        door.SetActive(false);

        tracker_dot = GameObject.Find("TrackerDot");
        tracker_dot.SetActive(false);

        spark = GetComponents<AudioSource>()[1];
        buzz = GetComponents<AudioSource>()[2];
        explode = GetComponents<AudioSource>()[3];

        tracker = new TrackAchievements();

        for (int i = 0; i < particles.Length; i += 2)
        {
            ParticleSystem.MainModule m = particles[i].main;
            m.startDelay = spark_delay;
        }

        StartCoroutine(LoadLevel());

	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

		if (time >= light_flicker_delay && time <= (light_flicker_delay + sushilogo[0].clip.length * light_flicker_times))
        {
            foreach(Animation a in sushilogo)
            {
                a.Play();
            }
            foreach(Animation a in lamp1)
            {
                a.Play();
            }
            foreach(Animation a in lamp2)
            {
                a.Play();
            }

            if (play_spark_sound && spark_sound_delay < sushilogo[0].clip.length)
            {
                spark.Play();
                play_spark_sound = false;
                Debug.Log("Play Spark");

            }
            else if (spark_sound_delay >= sushilogo[0].clip.length)
            {
                play_spark_sound = true;
                spark_sound_delay = 0f;
            }

            spark_sound_delay += Time.deltaTime;

        }

        if (time >= (light_flicker_delay + sushilogo[0].clip.length * light_flicker_times) && play_explode)
        {
            buzz.Stop();
            explode.Play();
            play_explode = false;
        }

        if (time >= alien_burst_delay && time <= (alien_burst_delay + chestburster[0].clip.length))
        {
            burst_spot.SetActive(true);
            foreach(Animation a in chestburster)
            {
                a.Play();
            }
            if (play_burst)
            {
                burst = GameObject.Find("XenomorphChestburst").GetComponent<AudioSource>();
                burst.Play();
                play_burst = false;
            }
        }
        if (time >= (alien_burst_delay + chestburster[0].clip.length + .01f))
            burst_spot.SetActive(false);

        if (time >= tracker_delay)
            tracker_dot.SetActive(true);

        if (time >= alien_dance_delay)
        {
            dance_alien.SetActive(true);
            spot_light.SetActive(true);
            door.SetActive(true);
        }

        if (time >= end_of_scene && !do_not_end)
        {
            //SceneManager.LoadScene(0, LoadSceneMode.Single);
            tracker.SaveLastLevel("alienz");
            loadchopstick = true;
        }
	}


    IEnumerator LoadLevel()
    {
        Debug.Log("LoadingLevel");
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

        async.allowSceneActivation = true;

    }
}
