using UnityEngine;
using System.Collections;


namespace UnityStandardAssets.Characters.FirstPerson
{
public class MouseLooker : MonoBehaviour {


	[SerializeField] private MouseLook m_MouseLook;
	private Camera m_Camera;

	// Use this for initialization
	void Start () {
		m_Camera = Camera.main;
		m_MouseLook.Init(transform , m_Camera.transform);
	}
	
	// Update is called once per frame
	void Update () {
		RotateView ();
	}

		void FixedUpdate(){
			m_MouseLook.UpdateCursorLock();
		}


	private void RotateView()
	{
		m_MouseLook.LookRotation (transform, m_Camera.transform);
	}


	/*private void UpdateCameraPosition(float speed)
	{
		Vector3 newCameraPosition;
		if (!m_UseHeadBob)
		{
			return;
		}
		if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
		{
			m_Camera.transform.localPosition =
				m_HeadBob.DoHeadBob(m_CharacterController.velocity.magnitude +
					(speed*(m_IsWalking ? 1f : m_RunstepLenghten)));
			newCameraPosition = m_Camera.transform.localPosition;
			newCameraPosition.y = m_Camera.transform.localPosition.y - m_JumpBob.Offset();
		}
		else
		{
			newCameraPosition = m_Camera.transform.localPosition;
			newCameraPosition.y = m_OriginalCameraPosition.y - m_JumpBob.Offset();
		}
		m_Camera.transform.localPosition = newCameraPosition;
		}*/

	}
}