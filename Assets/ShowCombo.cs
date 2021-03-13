using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCombo : MonoBehaviour
{

    private float timer = 0f;
    public float countdown_time = 1f;
    private bool showing_combo = false;
    public GameObject combo_spot, combo_popup;
    // Start is called before the first frame update
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {

        if (showing_combo)
        {

            timer -= Time.unscaledDeltaTime;
            if (timer <= countdown_time && !combo_popup.activeSelf)
            {
                Debug.Log("Showing Combo");
                combo_popup.SetActive(true);
                combo_popup.transform.position = combo_spot.transform.position;
                combo_popup.transform.rotation = combo_spot.transform.rotation;
                combo_popup.transform.localPosition += new Vector3(0, .1f, 0);
                //combo_popup.transform.localRotation = Quaternion.Euler(0, 180, 0);

            }
            if (timer <= 0)
            {
                showing_combo = false;
                combo_popup.SetActive(false);
            }
        }
    }

    public void ShowComboPopup()
    {
        showing_combo = true;
        timer = countdown_time;
    }
}
