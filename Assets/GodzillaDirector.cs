using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodzillaDirector : MonoBehaviour
{
    public float gravity_modifier;
    private Vector3 initial_gravity;
    // Start is called before the first frame update
    void Start()
    {
        initial_gravity = Physics.gravity;
    }

    // Update is called once per frame
    void Update()
    {
        Physics.gravity = initial_gravity * gravity_modifier;
    }
}
