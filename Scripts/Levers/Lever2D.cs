using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VR = UnityEngine.VR;


public class Lever2D : MonoBehaviour
{
	private GameObject hand;
	private GameObject handMesh;

	[SerializeField]
	private Vector3 handMeshPositionOffset = Vector3.zero;

	private OculusHaptics handHaptics;

	//IMPORTANT: anchor & zeroPoint must be on the same layer of the hierarchy because their LOCAL positions are compared
	[SerializeField]
	private Transform anchor = null;
	[SerializeField]
	private Transform zeroPoint = null;
	[SerializeField]
	private float zLimit = 0.1f;
	[SerializeField]
	private float xLimit = 0.1f;
	[SerializeField]
	private float range = 0.5f;
	[SerializeField]
	private float deadZone = 0.1f;
	[SerializeField]
	private float sensetivity = 100;

	private Vector3 difference;

	private bool isGrabbed;
	private bool justMovedHandMesh = false;

	private Vector3 handMeshStoredRotation;

	void  Start ()
	{
		difference = Vector3.zero;
		isGrabbed = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (isGrabbed) {
			if (Vector3.Distance (hand.transform.position, anchor.position) < range) {
		
				Vector3 direction = transform.position - hand.transform.position;

				//Face towards target
				transform.rotation = Quaternion.FromToRotation (Vector3.down, direction);

				//Find difference between current anchor and the zero point
				//This is for distance comparison
				//You can then compare distance on seperate axis
				difference = zeroPoint.localPosition - anchor.localPosition;

				if (difference.x > xLimit || difference.x < -xLimit || difference.z > zLimit || difference.z < -zLimit) {
					if ((difference.x > xLimit * 2 || difference.x < -xLimit * 2 || difference.z > zLimit * 2 || difference.z < -zLimit * 2)) {
						handHaptics.Vibrate (VibrationForce.Hard);
					} else if (difference.x > xLimit * 1.5 || difference.x < -xLimit * 1.5 || difference.z > zLimit * 1.5 || difference.z < -zLimit * 1.5) {
						handHaptics.Vibrate (VibrationForce.Medium);
					} else {
						handHaptics.Vibrate (VibrationForce.Light);
					}
					
				}
			}
		}
	}

	void LateUpdate(){
		if (isGrabbed) {
			handMesh.transform.position = anchor.position;
			handMesh.transform.localPosition += handMeshPositionOffset;
			justMovedHandMesh = true;
		} else if (justMovedHandMesh == true) {
			handMesh.transform.localPosition = Vector3.zero;
			handMesh.transform.localEulerAngles = handMeshStoredRotation;
			justMovedHandMesh = false;
		}
	}

	public float GetPitch ()
	{
		float pitch = difference.z;
		if (pitch > 0) {
			pitch -= deadZone;
			if (pitch < 0) {
				pitch = 0;
			}
		} else if (pitch < 0) {
			pitch += deadZone;
			if (pitch > 0) {
				pitch = 0;
			}
		}
		return -pitch * sensetivity;
	}

	public float GetYaw ()
	{
		float yaw = difference.x;
		if (yaw > 0) {
			yaw -= deadZone;
			if (yaw < 0) {
				yaw = 0;
			}
		} else if (yaw < 0) {
			yaw += deadZone;
			if (yaw > 0) {
				yaw = 0;
			}
		}
		return -yaw * sensetivity;
	}

	public void SetIsGrabbed(bool isGrabbedNew){
		isGrabbed = isGrabbedNew;
	}

	public void SetHand(GameObject newHand){
		hand = newHand;
		handMesh = hand.transform.GetChild (0).gameObject;
		handHaptics = hand.GetComponent<OculusHaptics> ();
		handMeshStoredRotation = handMesh.transform.localEulerAngles;
	}

}
