using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VR = UnityEngine.VR;
using System;

public class Grabbable : MonoBehaviour {

	public event Action<GameObject> grabStart;
	public event Action grabEnd;

	private bool isGrabbed;

	private GameObject hand;

	public void StartGrab(GameObject hand){
		this.hand = hand;
		isGrabbed = true;
		if (grabStart != null) {
			grabStart (hand);
		}
	}

	public void EndGrab(){
		isGrabbed = false;
		if (grabEnd != null) {
			grabEnd ();
		}
	}

	public GameObject GetHand(){
		return this.hand;
	}

	public bool IsGrabbed{
		get{ return isGrabbed; }
	}


}
