using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterJump : MonoBehaviour {

    public float jump_strength = 1f;
    public float jump_delay = 1f;
    public float force_radius = 1f;
    public Vector3 jump_offset;
    public bool do_jump = false;
    private float jump_timer = 0f;
    private GameObject scared_eyes, angry_eyes;
    private GameObject user_mouth;
    private Animator left_arm, right_arm, mouth;

	// Use this for initialization
	void Start () {
        jump_timer = 0f;

        scared_eyes = transform.Find("MonsterParts(Clone)/ScaredEyes").gameObject;
        angry_eyes = transform.Find("MonsterParts(Clone)/AngryEyes").gameObject;
        left_arm = transform.Find("MonsterParts(Clone)/LeftArmWiggle").gameObject.GetComponent<Animator>();
        right_arm = transform.Find("MonsterParts(Clone)/RightArmWiggle").gameObject.GetComponent<Animator>();
        mouth = transform.Find("MonsterParts(Clone)/MonsterMouth").gameObject.GetComponent<Animator>();

        user_mouth = GameObject.Find("ChopstickHolder/ChopstickStuff/OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor/MouthCollider");
    }
	
	// Update is called once per frame
	void Update () {

        //Jump Sequence
        if (jump_timer <= 0)
        {
            if (do_jump)
            {
                doJump();
                //Debug.Log("Jump!");
            }
            jump_timer = jump_delay;
        }
        else
            jump_timer -= Time.deltaTime;

        //Sushi is scared if grabbed
        doScaredSushi();
	}

    private void doJump()
    {
        GetComponent<Rigidbody>().AddExplosionForce(jump_strength, transform.position - jump_offset, force_radius);
    }

    private void doScaredSushi()
    {
        if (GetComponent<Grabbable>().isGrabbed)
        {
            scared_eyes.SetActive(true);
            angry_eyes.SetActive(false);
            left_arm.speed = 2.5f;
            right_arm.speed = 2.5f;
            mouth.speed = 3;
        }
        else
        {
            scared_eyes.SetActive(false);
            angry_eyes.SetActive(true);
            left_arm.speed = 1;
            right_arm.speed = 1;
            mouth.speed = 1;
        }
    }
}
