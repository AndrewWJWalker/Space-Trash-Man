using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;

public class HeadsetRotationDebug : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log ("Headset X: " + UnityEngine.VR.InputTracking.GetLocalRotation (VRNode.Head).eulerAngles.x);
		Debug.Log ("Headset Y: " + UnityEngine.VR.InputTracking.GetLocalRotation (VRNode.Head).eulerAngles.y);
		Debug.Log ("Headset Z: " + UnityEngine.VR.InputTracking.GetLocalRotation (VRNode.Head).eulerAngles.z);
	}
}
