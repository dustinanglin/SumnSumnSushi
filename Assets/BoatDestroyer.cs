using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatDestroyer : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other)
            if (other.gameObject)
                if (other.gameObject.transform.parent)
                {
                    //Debug.Log(other.gameObject.transform.parent.gameObject.transform.parent.gameObject.name + " has entered Destoyer!");
                    if (other.gameObject.transform.parent.gameObject.transform.parent.gameObject.name.Contains("SushiBoat"))
                        Destroy(other.gameObject.transform.parent.gameObject.transform.parent.gameObject,5);
                }
    }

}
