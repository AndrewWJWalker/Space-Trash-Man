using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOrbit : MonoBehaviour {

	[SerializeField]
	public Transform anchor = null;
	[SerializeField]
	public float speed = 0.25f;

	private Vector3 toAnchor;
	private float yDifference;
	private float yAngle;

	// Update is called once per frame
	void Update () {

		//find angle between this->planetoid and forward vector
		toAnchor = anchor.position - this.transform.position;
		yAngle = Vector3.Angle (toAnchor, this.transform.forward);

		//adjust local rotation to stay perpendicular to planetoid surface
		yDifference = 0;
		if (yAngle < 90) {
			yDifference = 90 - yAngle;
		} else if (yAngle > 90) {
			yDifference = -90 + yAngle;
		}

		this.transform.Rotate( new Vector3 (0, yDifference, 0), Space.Self);

		this.transform.position += this.transform.forward * speed;

	}
}
