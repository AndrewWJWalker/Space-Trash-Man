using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectDefault : MonoBehaviour {

	void Start () {
		SelectControlUI button = GetComponent<SelectControlUI> ();
		button.gazeInputEvent ();
	}

}
