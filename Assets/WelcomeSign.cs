using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeSign : MonoBehaviour {

    public Material welcome1, welcome2, welcome3;
    public GameObject player, left_hand, right_hand;
    public float welcome_time_1, welcome_time_2;
    private int stage;
    private bool staring_at_billboard;
    private bool thumbs_up;
    public Animator sign_animator;
    private RaycastHit hit;


	// Use this for initialization
	void Start () {
        stage = 1;
	}
	
	// Update is called once per frame
	void Update () {



        Physics.Raycast(player.transform.position, player.transform.forward, out hit);
        Debug.DrawRay(player.transform.position, player.transform.forward, Color.red);

        if (hit.collider != null)
            staring_at_billboard = (hit.collider.gameObject.name == "Boardface");

        thumbs_up = (left_hand.activeSelf || right_hand.activeSelf);

        switch (stage)
        {
            case 1:
                if (staring_at_billboard)
                {
                    welcome_time_1 -= Time.deltaTime;
                }
                if (welcome_time_1 <= 0)
                {
                    this.GetComponent<Renderer>().material = welcome2;
                    sign_animator.SetBool("showthumbs", true);
                    stage = 2;
                }
                    
                break;

            case 2:
                if (thumbs_up)
                {
                    welcome_time_2 -= Time.deltaTime;
                }
                if (welcome_time_2 <= 0)
                {
                    stage = 3;
                    this.GetComponent<Renderer>().material = welcome3;
                }
                break;

            case 3:
                Debug.Log("Game Start!");
                break;

        }
	}
}
