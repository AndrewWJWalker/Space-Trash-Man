using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIGazeBeginMain : MonoBehaviour {

	[SerializeField]
	private string scene = null;
	[SerializeField]
	private Text text = null;
	[SerializeField]
	private Fade fader = null;

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

	public void gazeEnterEvent(){
		text.color = Color.gray;
	}

	public void gazeLeaveEvent(){
		text.color = Color.white;
	}

	public void gazeInputEvent(){
		GlobalSettings.gameState = GameState.Main;
		fader.FadeToOpaque (() => {
			SceneManager.LoadScene (scene);
		});
	}

}
