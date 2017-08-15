using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;


public class HandRotationDebug : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		Debug.Log ("Hand Rotation: " + UnityEngine.VR.InputTracking.GetLocalRotation(VRNode.RightHand).eulerAngles);
	}
}
