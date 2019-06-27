using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour {

    public GameObject hex, director;
    private TronGameDirector win_director;
    private int hex_num = 0;
    public Material glow_red, glow_blue;
    public int columns, height = 0;
    public float horizontal_spacing, vertical_spacing = 0f;

	// Use this for initialization
	void Start () {
        win_director = director.GetComponent<TronGameDirector>();
        buildGrid();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void buildGrid()
    {
        for (int i = 0; i < columns; i++)
        {
            if (i % 2 == 0)
                for (int k = 0; k < height; k++)
                {
                    float offset = 0;
                    if (i % 2 == 0)
                        offset = vertical_spacing / 2;
                    Vector3 v = new Vector3(transform.position.x + i * horizontal_spacing, transform.position.y + k * vertical_spacing + offset, transform.position.z);
                    GameObject t_Hex = GameObject.Instantiate(hex, v, hex.transform.rotation, this.transform);
                    t_Hex.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = (Random.Range(0, 1.0f) > .5f) ? glow_red : glow_blue;
                    //Debug.Log("I made it this far in the loop");
                    hex_num++;
                }
            else
                for (int k = 0; k < height + 1; k++)
                {
                    float offset = 0;
                    if (i % 2 == 0)
                        offset = vertical_spacing / 2;
                    Vector3 v = new Vector3(transform.position.x + i * horizontal_spacing, transform.position.y + k * vertical_spacing + offset, transform.position.z);
                    GameObject t_Hex = GameObject.Instantiate(hex, v, hex.transform.rotation, this.transform);
                    t_Hex.transform.GetChild(0).gameObject.GetComponent<Renderer>().material = (Random.Range(0, 1.0f) > .5f) ? glow_red : glow_blue;
                    hex_num++;
                }
        }
        win_director.number_of_hex = hex_num;
        //Debug.Log("Number of hexes is " + hex_num);
    }
}
