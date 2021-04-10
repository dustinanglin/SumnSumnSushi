using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutoutMotor : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;

    [SerializeField]
    private bool motorOn, death;

    [SerializeField]
    private float deathClock;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //motorOn = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (motorOn)
            MoveForwardRB();

        if (death)
            DeathCountDown();

    }

    private void MoveForwardRB()
    {
        Vector3 move = transform.position + transform.forward * speed * Time.deltaTime;
        rb.MovePosition(move);
    }

    private void DeathCountDown()
    {
        deathClock -= Time.deltaTime;

        if (deathClock <= 0)
            Destroy(this.gameObject);
    }

    public void MotorOff()
    {
        motorOn = false;
    }

    public void MotorOn()
    {
        motorOn = true;
    }

    public void SetDeathTimer()
    {
        death = true;
    }

    public void SetSpeed(float sp)
    {
        speed = sp;
    }
}

