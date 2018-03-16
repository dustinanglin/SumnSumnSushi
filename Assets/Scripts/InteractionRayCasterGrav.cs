using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InteractionRayCasterGrav : MonoBehaviour {

	public float distanceToObject = 25;
	private Text targetText;
	private int layerMask;
	private bool carrying;
    private bool doRotate = false;
    private Quaternion qFlip;
    private GameObject wallMarker;
    private GameObject player;
    private Vector3 wallNormal;

	void Start () {
        wallMarker = GameObject.Find("SelectionCylinder");
        player = GameObject.Find("Player");
		//layerMask = 1 << 8; //Layer 8 is Ground
		//layerMask = ~layerMask; //We want the mask to be everything but 8
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawRay (transform.position, transform.forward * distanceToObject, Color.magenta);
        Debug.DrawRay(player.transform.position, player.transform.up * 5, Color.green);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, distanceToObject))
        //if (Physics.Raycast(transform.position, transform.forward, out hit, distanceToObject, layerMask))
        {
            wallMarker.SetActive(true);
            string collidingWith = "Hit distance" + hit.distance.ToString();
            //Debug.Log(collidingWith);

            wallMarker.transform.position = hit.point;
            wallMarker.transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            wallNormal = hit.normal;
            Debug.DrawRay(hit.point, hit.normal * 3, Color.cyan);
        }
        else
            wallMarker.SetActive(false);


        if (Physics.Raycast(transform.position, transform.forward, out hit, distanceToObject))
        {
            if (Input.GetButtonDown("Submit"))
            {
                doRotate = true;
                qFlip = Quaternion.FromToRotation(player.transform.up, hit.normal);
                Debug.Log(hit.normal);
                Debug.Log(player.transform.up);
                Debug.Log(qFlip);
                wallNormal = hit.normal;
            }
        }

        if (doRotate)
        {
            //Quaternion target = Quaternion.Euler (flipX, transform.rotation.eulerAngles.y, flip);
            player.transform.up = wallNormal; //= Vector3.Lerp(player.transform.up, wallNormal, Time.deltaTime * 10f);

            /*qFlip = Quaternion.RotateTowards(Quaternion.identity, qFlip, Time.deltaTime * 500f);
            player.transform.rotation = qFlip * player.transform.rotation;*/

            //Debug.Log(Quaternion.Angle(player.transform.rotation, qFlip));

            /*if (Quaternion.Angle(player.transform.rotation,qFlip) == 0)
            {
                Debug.Log("Rotate done");
                doRotate = false;
            }*/

            Debug.Log(player.transform.up);
            Debug.Log(wallNormal);
            Debug.Log(Vector3.Angle(player.transform.up, wallNormal));

            if (player.transform.up == wallNormal)
            {
                player.transform.up = wallNormal;
                doRotate = false;
            }
        }


    }

	/*void PickUp(){


		if (Physics.Raycast (transform.position, transform.forward, out hit, distanceToObject, layerMask)) {
			Pickupable p = hit.collider.GetComponent<Pickupable> ();
			if (p != null) {
				targetText.fontStyle = FontStyle.Bold;
				targetText.color = Color.red;
				targetText.fontSize = 24;
				collisionDistance = hit.distance;
				if (Input.GetMouseButtonDown (0)) {
					carriedObject = p.gameObject;
					carrying = true;
				}
			}

		}else {
			targetText.fontStyle = FontStyle.Normal;
			targetText.color = Color.black;
			targetText.fontSize = 20;
			collisionDistance = 0;
		}
	}*/

}
