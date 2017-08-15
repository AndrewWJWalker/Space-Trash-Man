using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorFunctions{

	public static float PolarAngleToFrom(Vector3 to, Vector3 from){
		float angle = Quaternion.FromToRotation (from, to).eulerAngles.x;
		if (angle > 180) {
			return angle - 360f;
		}
		return angle;
	}

}
