using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour {

    private Scene Chopstick, Superhot;
    private SaveandLoad saver;
    private TrackAchievements tracker;
    GameObject ChopstickHolder;
    private List<GameObject> hottext = new List<GameObject>();
    private bool loadsuper, loadxeno, loadtron, loadtrek = false;
    public float trektime, hottime = 1f;
    private float trektimelocal, hottimelocal = 1f;
    private int hoterator = 0;
    private bool tryload = false;
    private bool preloadlevel, loadlevel = false;

    private AudioSource trek_sound, alienz_sound;

	// Use this for initialization
	void Start () {
        tracker = GameObject.Find("AchievementTracker").GetComponent<TrackAchievements>();
        trek_sound = GetComponents<AudioSource>()[0];
        alienz_sound = GetComponents<AudioSource>()[1];

        for (int i = 1; i < 25; i++)
        {
            hottext.Add(GameObject.Find("HotTransition").transform.Find("hot" + i).gameObject);
        }
        hottimelocal = hottime;
        trektimelocal = trektime;
        hoterator = 0;


        string lastlevel = tracker.GetLastLevel();

        switch (lastlevel)
        {
            case "holodeck":
                trek_sound.Play();
                break;
            case "alienz":
                alienz_sound.Play();
                break;
            default:
                Debug.Log("No last level");
                break;
        }

        saver = GetComponent<SaveandLoad>();
        saver.LoadObjects();

	}
	
	// Update is called once per frame
	void Update ()
    {
        if (loadsuper)
            HotTransition();

        if (loadtrek)
            TrekTransition();

        if (loadtron)
            TronTransition();

        if (loadxeno)
            XenoTransition();


	}

    public void PreLoadLevel(string levelname)
    {
        StartCoroutine(LoadLevel(levelname));
    }

    IEnumerator LoadLevel(string levelname)
    {
        var async = SceneManager.LoadSceneAsync(levelname);
        async.allowSceneActivation = false;

        while (async.progress < .9f)
        {
            Debug.Log(async.progress);
            yield return null;
        }

        while (!loadlevel)
            yield return null;

        saver.SaveObjects();
        async.allowSceneActivation = true;
     }
    
    public void SetHot()
    {
        loadsuper = true;
    }

    public void SetXeno()
    {
        loadxeno = true;
    }

    public void SetTron()
    {
        loadtron = true;
    }

    public void SetTrek()
    {
        loadtrek = true;
    }

    private void TronTransition()
    {
        //SceneManager.LoadScene("TronTest", LoadSceneMode.Single);
        if (!preloadlevel)
        {
            preloadlevel = true;
            StartCoroutine(LoadLevel("Tron"));
        }
        loadlevel = true;
    }

    private void XenoTransition()
    {
        //SceneManager.LoadScene("Alienz", LoadSceneMode.Single);
        if (!preloadlevel)
        {
            preloadlevel = true;
            StartCoroutine(LoadLevel("Alienz"));
        }
        loadlevel = true;
    }

    private void TrekTransition()
    {
        if (!preloadlevel)
        {
            preloadlevel = true;
            StartCoroutine(LoadLevel("Holodeck"));
        }
        if (trektimelocal <= 0)
        {
            //SceneManager.LoadScene("Holodeck", LoadSceneMode.Single);
            loadlevel = true;
        }
        else
        {
            trektimelocal -= Time.deltaTime;
        }
    }

    void HotTransition()
    {
        if (!preloadlevel)
        {
            preloadlevel = true;
            StartCoroutine(LoadLevel("SushiHot"));
        }
        if (hottimelocal <= 0)
        {
            if (hoterator < hottext.Count)
                hottext[hoterator].SetActive(true);
            hottimelocal = hottime / (hoterator + 1);
            hoterator++;
        }
        hottimelocal -= Time.deltaTime;

        if (hoterator >= hottext.Count && !tryload)
        {
            tryload = true;
            //SceneManager.LoadScene(1, LoadSceneMode.Single);
            loadlevel = true;
        }
    }
}
