using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAlongSpline : MonoBehaviour {

	public BezierSpline spline;

	public float speed;
	public bool loop;

	private float progress;

	// Use this for initialization
	private void Start () {
		progress = 0;
	}
	
	// Update is called once per frame
	private void Update ()
	{
		progress = progress + (speed * Time.deltaTime);

		if (progress > 1f && loop) {
			progress = 0f;
		} else if (progress > 1f && !loop) {
			progress = 1f;
		}

		Vector3 position = spline.GetPoint (progress);
		transform.position = position;
		transform.LookAt (position + spline.GetDirection (progress));
	}
}


