using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour {

	public Transform target;
	public Vector3 rotationOffset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		this.transform.LookAt (this.target);
		this.transform.Rotate (this.rotationOffset);
	}
}
