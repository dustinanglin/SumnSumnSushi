using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFW : MonoBehaviour
{

    public float rotate_speed;
    private float rotate_speed_local;
    // Start is called before the first frame update
    void Start()
    {
        rotate_speed_local = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rotate_speed_local < rotate_speed)
        {
            rotate_speed_local += Time.deltaTime;
        }

        transform.Rotate(rotate_speed_local * Time.deltaTime, 0, 0);
    }
}
