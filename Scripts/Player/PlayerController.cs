using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VR = UnityEngine.VR;
using UnityEngine.SceneManagement;
using System;

public enum PlayerControlStates
{
	Joystick,
	Gamepad,
	Gaze
}

public class PlayerController : MonoBehaviour
{
	[SerializeField]
	private Lever1D speedJoystick = null;
	[SerializeField]
	private Lever2D controlJoystick = null;
	[SerializeField]
	private Lever1D rollJoystick = null;
	[SerializeField]
	private float minSpeed = 5f;
	[SerializeField]
	private float maxSpeed = 10f;
	[SerializeField]
	private float acellerationFactor = 0.0001f;
	[SerializeField]
	private float xboxAccelerationFactor = 0.01f;
	[SerializeField]
	private float decellerationFactor = 1f;
	[SerializeField]
	private float yawRotationFactor = 30f;
	[SerializeField]
	private float pitchRotationFactor = 30f;
	[SerializeField]
	private float rollRotationFactor = 50f;
	[SerializeField]
	private bool isYaw = true;
	[SerializeField]
	private bool isPitch = true;
	[SerializeField]
	private bool isRoll = true;
	[SerializeField]
	private float controllerSensitivity = 100f;

	private PlayerControlStates controlState;
	private float velocity;
	private float acceleration;
	private float yaw;
	/* Horizontal */
	private float pitch;
	/* Vertical */
	private float roll;
	/* Around Forward */

	public static PlayerController instance;

	void Awake(){
		if (instance != null) {
			GameObject.Destroy (instance);
		} else {
			instance = this;
		}
	}

	void Start ()
	{
		velocity = 0;
		acceleration = 0;
		yaw = 0;
		pitch = 0;
		roll = 0;
		controlState = GlobalSettings.controlState;
	}
	
	// FixedUpdate is called once per physics frame
	void FixedUpdate ()
	{

		switch (controlState) 
		{
			case PlayerControlStates.Gamepad:
				ControllerControl ();
				break;
			case PlayerControlStates.Gaze:
				GazeControl ();
				break;
			case PlayerControlStates.Joystick:
				JoystickControl ();
				break;
		}

		RotatePlayer ();
		MovePlayer ();	
	}


	void RotatePlayer ()
	{
		//rotate using local space instead of world space
		this.transform.Rotate (new Vector3 (pitch / pitchRotationFactor, yaw / yawRotationFactor, roll / rollRotationFactor), Space.Self);
	}



	void MovePlayer ()
	{
		if (velocity < maxSpeed) {
			velocity += acceleration;
		}

		if (velocity > maxSpeed) {
			velocity = maxSpeed;
		}

		if (velocity > minSpeed && acceleration == 0) {
			velocity -= decellerationFactor;
		}

		if (velocity < minSpeed) {
			velocity = minSpeed;
		}
	
		//move in the direction the player is currently facing
		this.transform.position += this.transform.forward * velocity;
	}

	private void GazeControl ()
	{
		acceleration = speedJoystick.GetZAxis() * acellerationFactor;

		Vector3 leftControllerPosition = VR.InputTracking.GetLocalPosition (VR.VRNode.LeftHand);
		Vector3 rightControllerPosition = VR.InputTracking.GetLocalPosition (VR.VRNode.RightHand);

		Vector3 betweenHands = leftControllerPosition - rightControllerPosition;
		if (acceleration > 0) {
			if (isRoll) {

				roll = Vector3.Angle (betweenHands, Vector3.up);

				if (roll == 90) {
					roll = 0;
				} else if (roll > 90) {
					roll = roll - 90;
				} else if (roll < 90) {
					roll = (-90) + roll;
				}
			}

			if (isPitch) {
				pitch = VR.InputTracking.GetLocalRotation (VR.VRNode.Head).eulerAngles.x;

				// 0 = flat, 360to180 = positive, 1to180 = negative
				if (pitch > 180) {
					pitch = (-360) + pitch;
				}
			}

			if (isYaw) {
				yaw = VR.InputTracking.GetLocalRotation (VR.VRNode.Head).eulerAngles.y;

				if (yaw > 180) {
					yaw = (-360) + yaw;
				}
			}
		} else {
			roll = 0;
			pitch = 0;
			yaw = 0;
		}
	}

	private void HandControl ()
	{
		pitch = VR.InputTracking.GetLocalRotation (VR.VRNode.RightHand).eulerAngles.x;
		if (pitch > 180) {
			pitch = -360 + pitch;
		}

		yaw = VR.InputTracking.GetLocalRotation (VR.VRNode.RightHand).eulerAngles.y;
		if (yaw > 180) {
			yaw = -360 + yaw;
		}
			
		roll = VR.InputTracking.GetLocalRotation (VR.VRNode.RightHand).eulerAngles.z / 100;
	}

	private void JoystickControl ()
	{
		acceleration = speedJoystick.GetZAxis() * acellerationFactor;
		pitch = controlJoystick.GetPitch ();
		yaw = controlJoystick.GetYaw ();
		roll = -rollJoystick.GetZAxis ();
	}

	private void ControllerControl ()
	{

		acceleration = Input.GetAxis ("XboxAccelerate") * xboxAccelerationFactor;
		pitch = Input.GetAxis ("XboxPitch") * controllerSensitivity;
		yaw = Input.GetAxis ("XboxYaw") * controllerSensitivity;
		if (Input.GetAxis ("XboxRollLeft") == 1) {
			roll = 1 * controllerSensitivity;
		}
		if (Input.GetAxis ("XboxRollRight") == 1) {
			roll = -1 * controllerSensitivity;
		}
		if (Input.GetAxis ("XboxRollRight") == 1 && Input.GetAxis ("XboxRollLeft") == 1) {
			roll = 0;
		}
		if (Input.GetAxis ("XboxRollRight") < 1 && Input.GetAxis ("XboxRollLeft") < 1) {
			roll = 0;
		}
	}

	public float GetVelocityAsPercentage ()
	{
		float onePercent = (maxSpeed - minSpeed) / 100;
		return (((velocity - minSpeed) / onePercent) / 100);
	}

}
