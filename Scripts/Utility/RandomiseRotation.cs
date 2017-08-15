using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomiseRotation : MonoBehaviour {

	void Start () {
		transform.rotation = Random.rotation;
	}

}
