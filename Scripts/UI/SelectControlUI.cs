using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectControlUI : MonoBehaviour {

	[SerializeField]
	private PlayerControlStates state;
	[SerializeField]
	private Text text;
	[SerializeField]
	private Text backgroundText;
	[SerializeField]
	private Text otherText1;
	[SerializeField]
	private Text otherText2;

	private VRInteractable interactable;

	// Use this for initialization
	void OnEnable () {
		interactable = GetComponent<VRInteractable> ();
		interactable.gazeEnter += gazeEnterEvent;
		interactable.gazeLeave += gazeLeaveEvent;
		interactable.gazeInput += gazeInputEvent;
	}

	// Update is called once per frame
	void OnDisable () {
		interactable.gazeEnter -= gazeEnterEvent;
		interactable.gazeLeave -= gazeLeaveEvent;
		interactable.gazeInput -= gazeInputEvent;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void gazeEnterEvent(){
		text.color = Color.gray;
	}

	public void gazeLeaveEvent(){
		text.color = Color.white;
	}

	public void gazeInputEvent(){
		GlobalSettings.controlState = state;
		backgroundText.color = Color.red;
		otherText1.color = Color.black;
		otherText2.color = Color.black;
	}
}
