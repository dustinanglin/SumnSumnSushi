using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushaShake : MonoBehaviour
{
    public float shakeIntensity;
    public float shakeSpeed;
    public float shakeInensityFade;
    private float time, x_initial, z_initial;
    private bool initiated = false;
    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (!initiated) //timing wise, it's best to avoid a race condition by getting initial values here
        {
            initiated = true;
            x_initial = transform.position.x;
            z_initial = transform.rotation.eulerAngles.z;
        }

        if (this.enabled)
        {
            //this.transform.position = new Vector3(this.transform.position.x + Random.Range(-shakeIntensity, shakeIntensity), this.transform.position.y + Random.Range(-shakeIntensity, shakeIntensity), this.transform.position.z);
            this.transform.position = new Vector3(x_initial + (shakeIntensity / (time * shakeInensityFade)) * Mathf.Sin(time * shakeSpeed), this.transform.position.y, this.transform.position.z);
            this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, this.transform.eulerAngles.y, z_initial + 100 * (shakeIntensity / (time * shakeInensityFade)) * Mathf.Sin(time * shakeSpeed));
            //this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + (shakeIntensity / (time * shakeInensityFade)) * Mathf.Sin(time * shakeSpeed), this.transform.position.z);
        }
    }

    private void OnEnable()
    {
        time = 0;
        initiated = false;
    }
}
