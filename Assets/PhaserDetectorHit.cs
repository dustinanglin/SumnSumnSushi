using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaserDetectorHit : MonoBehaviour {

    public bool miss, needsReset = false;
    private PhaserGame game_manager;
    public GameObject gm_object;

    // Use this for initialization
    void Start()
    {
        game_manager = gm_object.GetComponent<PhaserGame>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if ((other.name.Contains("Wall") || other.name.Contains("Top") || other.name.Contains("Bottom")) && !miss && !needsReset)
        {
            miss = true;
            needsReset = true;
            Debug.Log("Miss!");
            game_manager.playFail();
        }

        if (other.name.Contains("Sphere") && !miss && !needsReset)
        {
            if(other.name.Contains("Point"))
            {
                Destroy(other.gameObject);
            }
            else if (other.name.Contains("Target"))
            {
                Destroy(other.gameObject.transform.parent.gameObject);
            }
            needsReset = true;
            Debug.Log(other.name);
            game_manager.playSuccess();
        }
    }

}
