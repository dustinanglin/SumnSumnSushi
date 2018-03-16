using UnityEngine;
using System.Collections;

public class CustomFirstPersonController : MonoBehaviour {

	public float moveSpeed;
	public float maxUp;
	public float maxDown;
	public float sensitivityY;
	public float sensitivityX;
	public bool inverted;
	public float gravity;
	public float jumpSpeed;
	public float airSpeed;
    public float distanceToObject = 25;
	//public float mass;


	private CharacterController m_CharacterController;
	private Camera m_Camera;
	private CollisionFlags m_CollisionFlags;

	private float forwardSpeed;
	private float sideSpeed;
	private float rotationX;
	private float rotationY;
	private float desiredY;
	private float velocityY;
	private float airMod;
	private float flip = 0f;
    private float flipX = 0f;

	private Vector3 eulerY;
	private Vector3 forward;
	private Vector3 upNormal;

	private bool isJumping;
	private bool jump;
	private bool doRotate = false;

	private Quaternion m_CameraTargetRot;

	// Use this for initialization
	void Start () {
		m_CharacterController = GetComponent<CharacterController> ();
		m_Camera = Camera.main;
		moveSpeed = 10f;
		maxUp = -90f;
		maxDown = 90f;
		sensitivityY = 3f;
		sensitivityX = 2f;
		desiredY = 0f;
		gravity = -9.8f;
		jumpSpeed = 9f;
		//mass = 60f;
		velocityY = 0f;
		airSpeed = 5f;
		isJumping = false;
		inverted = false;
		m_CameraTargetRot = m_Camera.transform.localRotation;
		eulerY = m_Camera.transform.localRotation.eulerAngles;
		upNormal = new Vector3 (0, 1, 0);
	}
	
	// Update is called once per frame
	void Update () {
		//Rotation
		rotationX = Input.GetAxis ("Mouse X") * sensitivityX;
		rotationY = Input.GetAxis ("Mouse Y") * sensitivityY;

        if (Input.GetButtonDown("LeftBumper"))
        {
            doRotate = true;
            flip -= 90;
            flip = flip % 360;
        }

        if (Input.GetButtonDown("RightBumper"))
        {
            doRotate = true;
            flip += 90;
            flip = flip % 360;
        }

        if (Input.GetButtonDown("B_Button"))
        {
            doRotate = true;
            flipX -= 90;
            flipX = flipX % 360;
        }

		if (doRotate) {
			Quaternion target = Quaternion.Euler (flipX, transform.rotation.eulerAngles.y, flip);
			transform.rotation = Quaternion.Slerp (transform.rotation, target, Time.deltaTime * 6f);
            Debug.Log("Rotating");
            if (Quaternion.Angle(transform.rotation, target) == 0)
                doRotate = false;
		}

		//Create a desired direction in degress (-90 is looking up, 90 is looking down)
		if (inverted)
			desiredY -= rotationY;
		else
			desiredY += rotationY;

		//Clamp the number to the max viewing angles
		desiredY = Mathf.Clamp (desiredY, maxUp, maxDown);

		//rotate horizontally
		transform.Rotate (0, rotationX, 0);

		//set the euler angle of the desired vertical rotation
		eulerY.x = desiredY;

		//rotate the camera to the desired euler vector
		m_Camera.transform.localRotation = Quaternion.Euler (eulerY);

		//Movement
		forwardSpeed = Input.GetAxis ("Vertical");
		sideSpeed = Input.GetAxis ("Horizontal");

		if (m_CollisionFlags == CollisionFlags.Below || m_CollisionFlags == CollisionFlags.Above || m_CollisionFlags == CollisionFlags.Sides) {
			velocityY = 0;
			isJumping = false; 
			//Debug.Log ("On bottom");
		} else {
			velocityY += gravity * Time.deltaTime;
		}



		//Debug.Log (velocityY);

		//Jumping
		if (Input.GetButtonDown("Jump") && !isJumping) {
			velocityY = jumpSpeed;
			isJumping = true;
		}

		if (isJumping)
			forward = new Vector3 (sideSpeed * airSpeed, velocityY, forwardSpeed * airSpeed);
		else
			forward = new Vector3 (sideSpeed * moveSpeed, velocityY, forwardSpeed * moveSpeed);

		forward = transform.rotation * forward;

		m_CollisionFlags = m_CharacterController.Move (forward * Time.deltaTime);

	}
		

}
