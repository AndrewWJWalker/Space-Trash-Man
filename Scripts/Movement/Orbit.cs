using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

	[SerializeField]
	private Transform anchor;
	[SerializeField]
	private float speed = 2.0f;

	private Vector3 toPlanetoid;
	private float xDifference;
	private float xAngle;

	void Start () {
		this.transform.LookAt (anchor);
		this.transform.Rotate (new Vector3 (90,0,0), Space.Self);
	}

	void Update () {
		//find angle between this->planetoid and forward vector
		toPlanetoid = anchor.position - this.transform.position;
		xAngle = Vector3.Angle (toPlanetoid, this.transform.forward);

		//adjust local rotation to stay perpendicular to planetoid surface
		xDifference = 0;
		if (xAngle < 90) {
			xDifference = 90 - xAngle;
		} else if (xAngle > 90) {
			xDifference = -90 + xAngle;
		}

		this.transform.Rotate( new Vector3 (xDifference, 0, 0), Space.Self);

		this.transform.position += this.transform.forward * speed;
	}
}
