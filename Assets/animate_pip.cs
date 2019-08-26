using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animate_pip : MonoBehaviour {

    public float min_bound, max_bound, move_speed;
    public bool do_animate = false;
    private bool animating = false;
    private float new_location = 0f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (do_animate)
        {
            new_location = Random.Range(min_bound, max_bound);
            do_animate = false;
            animating = true;
        }

		if (animating)
        {
            movePip();
        }
	}

    private void movePip()
    {
        Vector3 move_spot = new Vector3(new_location, transform.localPosition.y, transform.localPosition.z);
        transform.localPosition = Vector3.Lerp(transform.localPosition, move_spot, Time.deltaTime * move_speed);
        if (Vector3.Distance(transform.localPosition, move_spot) <= 0)
            animating = false;
    }
}
