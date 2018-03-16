using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour {

    private Scene Chopstick, Superhot;
    GameObject ChopstickHolder;
    private List<GameObject> hottext = new List<GameObject>();
    private bool loadsuper = false;
    public float hottime = 1f;
    private float hottimelocal = 1f;
    private int hoterator = 0;
    private bool tryload = false;

	// Use this for initialization
	void Start () {
        for (int i = 1; i < 25; i++)
        {
            hottext.Add(GameObject.Find("HotTransition").transform.Find("hot" + i).gameObject);
        }
        hottimelocal = hottime;
        hoterator = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (loadsuper)
            HotTransition();
	}
    
    public void SetHot()
    {
        loadsuper = true;
    }

    void HotTransition()
    {
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
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
}
