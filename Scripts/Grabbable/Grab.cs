using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VR = UnityEngine.VR;

public class Grab : MonoBehaviour {

	[SerializeField]
	private string grabAxis = "GrabRight";

	private Ray ray;
	private RaycastHit hit;

	private Grabbable currentGrabbable;
	private Grabbable previousGrabbable;

	private bool isGrabButtonHeld;
	private bool wasGrabJustHeld;
	private bool wasGrabbaleCheckJustTriggered;

	// Use this for initialization
	void Start () {
		isGrabButtonHeld = false;
		wasGrabbaleCheckJustTriggered = false;
		wasGrabJustHeld = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis (grabAxis) > 0.5) {
			isGrabButtonHeld = true;
		} else {
			if (wasGrabJustHeld) {
				if (currentGrabbable != null && currentGrabbable.GetHand() == this.gameObject) {
					currentGrabbable.EndGrab ();
					previousGrabbable = currentGrabbable;
					currentGrabbable = null;
				}
			}
			isGrabButtonHeld = false;
			wasGrabbaleCheckJustTriggered = false;
			wasGrabJustHeld = false;
		}

		//only check for grabbable when trigger has just been held down
		if (isGrabButtonHeld == true && wasGrabbaleCheckJustTriggered == false){
			wasGrabJustHeld = true;
			wasGrabbaleCheckJustTriggered = true;
			//do grabbable checks
			Debug.Log("Checking for grabbable");
//			ray = new Ray (transform.position, transform.forward);
//			if (Physics.SphereCast(ray,radius, out hit, range)){
//				currentGrabbable = hit.collider.gameObject.GetComponent<Grabbable> ();
//				if (currentGrabbable != null) {
//					currentGrabbable.StartGrab ();
//				}
//			}
			if (currentGrabbable != null && currentGrabbable.IsGrabbed == false) {
				currentGrabbable.StartGrab (this.gameObject);
			}
		}
	}

	void OnTriggerEnter(Collider hit){
		Debug.Log ("Grab trigger entered");
		currentGrabbable = hit.gameObject.GetComponent<Grabbable> ();
	}

	void OnTriggerStay(Collider hit){
		if (currentGrabbable == null) {
			currentGrabbable = previousGrabbable;
		}
	}
}
