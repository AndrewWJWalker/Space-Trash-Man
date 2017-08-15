using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeSelection : MonoBehaviour {

	//Throw Ray forward from the center of the camera
	//detect if anything interactable was hit
	//if it was, send hit event
	//if nothing was hit & something was just hit, send leave event
	//sent input events when appropriate

	//NOTE: Make sure VRInteractables have a collider on them

	[SerializeField]
	private LayerMask _raycastMask = new LayerMask ();
	[SerializeField]
	private float _rayLength = 400f;

	private Ray _ray;
	private RaycastHit _hit;

	private VRInteractable _interactable;
	private VRInteractable _previousInteractable;

	private bool _isGazeActive;

	// Use this for initialization
	void Start () {
		_isGazeActive = false;
	}
	
	// Doing raycasting in Update instead of FixedUpdate because it's a costly procedure
	void Update () {

		_ray = new Ray (this.transform.position, this.transform.forward);

		if (Physics.Raycast (_ray, out _hit, _rayLength, _raycastMask)) {
			if (_isGazeActive == false) {
				_isGazeActive = true;
				_previousInteractable = null;
			}
			//find the interactable
			_interactable = _hit.collider.gameObject.GetComponent<VRInteractable> ();
			//only activate enter event if it's the first time looking at it
			if (_previousInteractable != _interactable) {
				_previousInteractable = _interactable;
				_interactable.enter ();
			}
			//if input is pressed, activate event
			if (Input.GetAxis ("Submit") == 1) {
				_interactable.input();
			}
		} else {
			//only activate leave event if it's the first time looking away
			if (_isGazeActive) {
				_interactable.leave ();
				_isGazeActive = false;
			}
		}

	}
}
