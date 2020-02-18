using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuckHunt : MonoBehaviour {

    public int max_shots;
    public float dog_delay;
    private AudioSource[] end_sounds;
    private Animator dog_peep, dog_laugh;
    private float animation_time, dog_delay_time;
    private bool deadduck, duck_off_screen, gameover, play_endsound;
    private int shots_remaining;
    private GameObject duck, dog, left_zapper, right_zapper, chop_left, chop_left_shadow, chop_right, chop_right_shadow;
    private SpriteRenderer shot_indicator;
    private Sprite[] shots;

	// Use this for initialization
	void Start () {
        right_zapper = GameObject.Find("RightHandAnchor").transform.Find("Zapper Right").gameObject;
        left_zapper = GameObject.Find("LeftHandAnchor").transform.Find("Zapper Left").gameObject;

        chop_right = GameObject.Find("Chopsticks_Right");
        chop_right_shadow = GameObject.Find("Chopsticks_Right_Shadow");
        chop_left = GameObject.Find("Chopsticks_Left");
        chop_left_shadow = GameObject.Find("Chopsticks_Left_Shadow");

        shot_indicator = transform.Find("DuckHuntShots").GetComponent<SpriteRenderer>();

        end_sounds = GetComponents<AudioSource>();
        gameover = true;

        shots = new Sprite[4];
        shots[0] = Resources.Load<Sprite>("shots_0");
        shots[1] = Resources.Load<Sprite>("shots_1");
        shots[2] = Resources.Load<Sprite>("shots_2");
        shots[3] = Resources.Load<Sprite>("shots_3");

        //InitiateGame();
    }
	
	// Update is called once per frame
	void Update () {
        if (!gameover)
        {
            if (shots_remaining <= 0 && !deadduck && !duck_off_screen)
                duck.GetComponent<DuckFly>().FlyAway();
            else if ((shots_remaining <=0 && duck_off_screen) || (deadduck && duck_off_screen))
                EndGame();
        }
        DebugControls();
	}

    private void DebugControls()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DecrementShots();
        }
    }

    public void DuckIsDead()
    {
        deadduck = true;
    }

    public void DuckOffScreen()
    {
        duck_off_screen = true;
    }

    public bool isGameRunning()
    {
        return !gameover;
    }

    public void InitiateGame()
    {
        //create duck and dog
        duck = Instantiate((GameObject)Resources.Load("Duck"), transform.position, transform.rotation);

        Transform dog_spot = GameObject.Find("DogSpot").transform;
        dog = Instantiate((GameObject)Resources.Load("Dog"), dog_spot.position, dog_spot.rotation);
        dog_peep = dog.transform.Find("DogMover").GetComponent<Animator>();
        dog_laugh = dog.transform.Find("DogMover/Dog").GetComponent<Animator>();

        //enable both zappers
        right_zapper.SetActive(true);
        left_zapper.SetActive(true);

        right_zapper.GetComponent<ZapperShoot>().InitiateDuck(duck.GetComponent<DuckFly>());
        left_zapper.GetComponent<ZapperShoot>().InitiateDuck(duck.GetComponent<DuckFly>());

        //disable chopsticks
        chop_left.SetActive(false);
        chop_left_shadow.SetActive(false);
        chop_right.SetActive(false);
        chop_right_shadow.SetActive(false);

        //enable shots indicator
        shot_indicator.gameObject.SetActive(true);

        //set inital game conditions
        shots_remaining = max_shots;
        dog_delay_time = dog_delay;
        deadduck = false;
        gameover = false;
        play_endsound = false;
        duck_off_screen = false;
        shot_indicator.sprite = shots[shots_remaining];

        AnimationClip[] clips;
        clips = dog_peep.runtimeAnimatorController.animationClips;

        animation_time = clips[0].length; //Maybe?
    }

    public void DecrementShots()
    {
        shots_remaining--;
        if (shots_remaining >= 0)
            shot_indicator.sprite = shots[shots_remaining];
        Debug.Log(shots_remaining);
    }

    public int ShotsRemaining()
    {
        return shots_remaining;
    }

    private void EndGame()
    {
        if (dog_delay_time > 0)
        {
            dog_delay_time -= Time.deltaTime;
        }
        else {
            dog_peep.SetBool("peep", true);

            if (deadduck)
            {
                if (!play_endsound)
                {
                    play_endsound = true;
                    end_sounds[0].Play();
                }
                dog_laugh.SetBool("laugh", false);
            }
            else
            {
                if (!play_endsound)
                {
                    end_sounds[1].Play();
                    play_endsound = true;
                }
                dog_laugh.SetBool("laugh", true);
            }

            if (animation_time > 0)
            {
                animation_time -= Time.deltaTime;
            }
            else
            {
                right_zapper.SetActive(false);
                left_zapper.SetActive(false);

                chop_left.SetActive(true);
                chop_left_shadow.SetActive(true);
                chop_right.SetActive(true);
                chop_right_shadow.SetActive(true);

                shot_indicator.gameObject.SetActive(false);

                Destroy(duck);
                Destroy(dog);
                gameover = true;
            }
        }
    }
}
