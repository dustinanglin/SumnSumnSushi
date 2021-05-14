using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingManager : MonoBehaviour
{
    [SerializeField]
    private Material trek, hot, tron, alien, godzilla;

    [SerializeField]
    private GameObject trekBottle, hotBottle, tronBottle, alienBottle, godzillaBottle;

    private GameObject trekButton, hotButton, alienButton, tronButton, godzillaButton;


    // Start is called before the first frame update
    void Start()
    {
        trekButton = transform.Find("Button2Holder").gameObject;
        hotButton = transform.Find("Button3Holder").gameObject;
        alienButton = transform.Find("Button7Holder").gameObject;
        tronButton = transform.Find("Button8Holder").gameObject;
        godzillaButton = transform.Find("Button9Holder").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UnlockSauce(string sauce)
    {
        VendingButton button;

        switch (sauce)
        {
            case "trek":
                trekButton.transform.Find("SauceLabel").GetComponent<MeshRenderer>().material = trek;
                button = trekButton.transform.Find("Button").GetComponent<VendingButton>();
                button.sauce_bottle = trekBottle;
                button.enabled = true;
                trekButton.transform.Find("PostItHolder").gameObject.SetActive(false);
                break;

            case "tron":
                tronButton.transform.Find("SauceLabel").GetComponent<MeshRenderer>().material = tron;
                button = tronButton.transform.Find("Button").GetComponent<VendingButton>();
                button.sauce_bottle = tronBottle;
                button.enabled = true;
                tronButton.transform.Find("PostItHolder").gameObject.SetActive(false);
                break;

            case "hot":
                hotButton.transform.Find("SauceLabel").GetComponent<MeshRenderer>().material = hot;
                button = hotButton.transform.Find("Button").GetComponent<VendingButton>();
                button.sauce_bottle = hotBottle;
                button.enabled = true;
                hotButton.transform.Find("PostItHolder").gameObject.SetActive(false);
                break;

            case "xeno":
                alienButton.transform.Find("SauceLabel").GetComponent<MeshRenderer>().material = alien;
                button = alienButton.transform.Find("Button").GetComponent<VendingButton>();
                button.sauce_bottle = alienBottle;
                button.enabled = true;
                alienButton.transform.Find("PostItHolder").gameObject.SetActive(false);
                break;

            case "godzilla":
                godzillaButton.transform.Find("SauceLabel").GetComponent<MeshRenderer>().material = godzilla;
                button = godzillaButton.transform.Find("Button").GetComponent<VendingButton>();
                button.sauce_bottle = godzillaBottle;
                button.enabled = true;
                godzillaButton.transform.Find("PostItHolder").gameObject.SetActive(false);
                break;
        }
    }
}
