using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscInstructions : MonoBehaviour {

    private TextMesh instructions;
    private GameObject head;
    private int show;

    [TextArea]
    public string inst_1, inst_2;

    // Use this for initialization
    void Start () {
        instructions = this.GetComponent<TextMesh>();
        show = 1;
        head = GameObject.Find("CenterEyeAnchor");
	}
	
	// Update is called once per frame
	void Update () {
        switch (show)
        {
            case 1:
                instructions.text = inst_1;
                break;
            case 2:
                instructions.text = inst_2;
                //this.gameObject.transform.rotation = this.gameObject.transform.parent.gameObject.transform.rotation;
                this.transform.localPosition = new Vector3(0, 0, 0.22f);
                this.transform.rotation = Quaternion.LookRotation(this.transform.position - head.transform.position);
                break;
            case 3:
                instructions.gameObject.SetActive(false);
                break;
            default:
                break;
        }
	}

    public void SetInstructionText(int mode)
    {
        show = mode;
    }
}
