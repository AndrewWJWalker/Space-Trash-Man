using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VRInteractable : MonoBehaviour {

	//add this to gameObjects which need  to be interacted  with via gaze
	//the gaze selection script will call methods in this class
	//other classes can subscribe to the object to act on inputs



	public event Action gazeEnter;
	public event Action gazeLeave;
	public event Action gazeInput;

	private bool isActive = false;

	public bool IsActive {
		get {
			return isActive;
		}
	}

	//the GazeSelection calls these classes, which activate the respective actions
	//any listeners to these actions will be notified
	public void enter(){
		this.isActive = true;
		//check if event has subscribers before activating
		if (gazeEnter != null) {
			gazeEnter();
		}
	}
	public void leave(){
		this.isActive = false;
		//check if event has subscribers before activating
		if (gazeLeave != null) {
			gazeLeave();
		}
	}
	public void input(){
		if (this.isActive) {
			if (gazeInput != null) {
				gazeInput ();
			}
		}
	}

}
