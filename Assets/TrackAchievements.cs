using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;


public class TrackAchievements : MonoBehaviour {

    private string gameDataFileName = "playerdata.json";

	// Use this for initialization
	void Start () {
        //DontDestroyOnLoad(gameObject);
        //LogLastLevel();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LogLastLevel()
    {
        if (PlayerPrefs.HasKey("lastlevel"))
        {
            Debug.Log("Last level was " + PlayerPrefs.GetString("lastlevel"));
            PlayerPrefs.DeleteKey("lastlevel");
            Debug.Log("Last level cleared");
        }
        else
            Debug.Log("No last level");
    }

    private void LoadPlayerProgress()
    {

    }

    public void SavePlayerProgress(string achievement)
    {

    }

    public void SaveLastLevel(string lastlevel)
    {
        PlayerPrefs.SetString("lastlevel", lastlevel);
    }

    public string GetLastLevel()
    {
        string lastlevel = "";

        if (PlayerPrefs.HasKey("lastlevel"))
        {
            lastlevel = PlayerPrefs.GetString("lastlevel");
            PlayerPrefs.DeleteKey("lastlevel");
        }

        return lastlevel;
    }
}
