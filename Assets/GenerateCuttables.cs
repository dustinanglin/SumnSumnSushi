using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCuttables : MonoBehaviour
{
    public GameObject Cutout;

    [SerializeField]
    private float generationDelay, delayLowerBound, delayUpperBound, initialSpeed;

    [SerializeField]
    private bool randomDelay;

    private float speedMod, speedTemp = 0f;
    private float genTime;
    // Start is called before the first frame update
    void Start()
    {
        setDelay();
    }

    // Update is called once per frame
    void Update()
    {
        genTime -= Time.deltaTime;
        if (genTime <= 0)
        {
            setDelay();
            CreateCuttable();
        }

        //every 10 seconds increase speed mod by .5
        speedTemp += Time.deltaTime / 10f;
        speedMod = Mathf.Floor(speedTemp) / 2f;
    }

    private void CreateCuttable()
    {
        GameObject go = GameObject.Instantiate(Cutout, transform.position, transform.rotation, null);
        go.GetComponent<CutoutMotor>().MotorOn();
        go.GetComponent<CutoutMotor>().SetSpeed(initialSpeed + speedMod);
    }

    private void setDelay()
    {
        if (randomDelay)
            genTime = Random.Range(delayLowerBound, delayUpperBound);
        else
            genTime = generationDelay;
    }
}
