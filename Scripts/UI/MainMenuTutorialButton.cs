using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuTutorialButton : MonoBehaviour {

	[SerializeField]
	private string gamePadScene = null;
	[SerializeField]
	private string gazeScene = null;
	[SerializeField]
	private string joystickScene = null;

	[SerializeField]
	private Text text = null;
	[SerializeField]
	private Fade fader = null;

	private string scene;
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
		switch (GlobalSettings.controlState) {
		case PlayerControlStates.Gamepad:
			scene = gamePadScene;
			break;
		case PlayerControlStates.Gaze:
			scene = gazeScene;
			break;
		case PlayerControlStates.Joystick:
			scene = joystickScene;
			break;
		}
		GlobalSettings.gameState = GameState.Tutorial;
		fader.FadeToOpaque (() => {
			SceneManager.LoadScene (scene);
		});
	}
}
