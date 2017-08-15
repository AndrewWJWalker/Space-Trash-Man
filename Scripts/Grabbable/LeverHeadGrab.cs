using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Grabbable))]
public class LeverHeadGrab : MonoBehaviour {

	[SerializeField]
	private Lever2D lever = null;
	private Grabbable grabbable;

	void OnEnable () {
		grabbable = GetComponent<Grabbable> ();
		grabbable.grabStart += HandleGrabStart;
		grabbable.grabEnd += HandleGrabEnd;
	}

	void OnDisable () {
		grabbable.grabStart -= HandleGrabStart;
		grabbable.grabEnd -= HandleGrabEnd;
	}

	public void HandleGrabStart(GameObject hand){
		Debug.Log ("Lever Got Grabbed!");
		lever.SetHand (hand);
		lever.SetIsGrabbed (true);
	}

	public void HandleGrabEnd(){
		Debug.Log ("Lever Got Ungrabbed!");
		lever.SetIsGrabbed (false);
	}
}
