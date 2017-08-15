using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever1D : MonoBehaviour
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
	private Transform anchorRestricted = null;
	[SerializeField]
	private Transform anchorBase = null;
	[SerializeField]
	private Transform zeroPoint = null;
	[SerializeField]
	private float rotationLimitPositive = 40f;
	[SerializeField]
	private float rotationLimitNegative = 0f;
	[SerializeField]
	private float zLimit = 0.1f;
	[SerializeField]
	private float xLimit = 0.1f;
	[SerializeField]
	private float deadZone = 0.1f;
	[SerializeField]
	private float sensetivity = 100;
	[SerializeField]
	private float movementSensetivity = 100;

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

			anchorRestricted.transform.position = hand.transform.position;
	
			Vector3 handToBase = anchorRestricted.localPosition - anchorBase.localPosition;
			Vector3 anchorToBase = anchor.localPosition - anchorBase.localPosition;

			float xDifference = VectorFunctions.PolarAngleToFrom (handToBase, anchorToBase);

			transform.Rotate (xDifference / movementSensetivity, 0, 0, Space.Self);

			float localX = transform.localEulerAngles.x;
			if (localX > 180) {
				localX = -360 + localX;
			}

			if (localX > rotationLimitPositive) {
				transform.Rotate (-xDifference / movementSensetivity, 0, 0, Space.Self);
			} else if (localX < rotationLimitNegative) {
				transform.Rotate (-xDifference / movementSensetivity, 0, 0, Space.Self);
			}

			difference = zeroPoint.localPosition - anchor.localPosition;

			if (difference.z > xLimit || difference.z < -zLimit) {
				if ((difference.z > zLimit * 2 || difference.z < -zLimit * 2)) {
					handHaptics.Vibrate (VibrationForce.Hard);
				} else if (difference.z > zLimit * 1.5 || difference.z < -zLimit * 1.5) {
					handHaptics.Vibrate (VibrationForce.Medium);
				} else {
					handHaptics.Vibrate (VibrationForce.Light);
				}

			}
		
		}
	}

	void LateUpdate ()
	{
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

	public float GetZAxis ()
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


	public void SetIsGrabbed (bool isGrabbedNew)
	{
		isGrabbed = isGrabbedNew;
	}

	public void SetHand (GameObject newHand)
	{
		hand = newHand;
		handMesh = hand.transform.GetChild (0).gameObject;
		handHaptics = hand.GetComponent<OculusHaptics> ();
		handMeshStoredRotation = handMesh.transform.localEulerAngles;
	}
		

}
