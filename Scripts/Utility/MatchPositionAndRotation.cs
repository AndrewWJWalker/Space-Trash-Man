using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchPositionAndRotation : MonoBehaviour {

	[SerializeField]
	private GameObject target = null;
	[SerializeField]
	private bool isPosition = true;
	[SerializeField]
	private bool isRotation = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (isPosition) {
			this.transform.position = target.transform.position;
		}
		if (isRotation) {
			this.transform.rotation = target.transform.rotation;
		}
	}
}
