using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerEventManagerMain : MonoBehaviour {

	[SerializeField]
	private DialougeManager dialougeManager = null;
	[SerializeField]
	private string[] introDialouge = null;
	[SerializeField]
	private string[] deathDialouge = null;
	[SerializeField]
	private string [] endDialouge = null;
	[SerializeField]
	private float cooldownLength = 10;
	[SerializeField]
	private float initialDelay = 1f;
	[SerializeField]
	private Fade fade = null;

	public static PlayerEventManagerMain instance;

	private string[] currentDialouges;
	private int currentString;
	private float cooldown;

	void Awake(){
		if (instance != null) {
			GameObject.Destroy (instance);
		} else {
			instance = this;
		}
		//DontDestroyOnLoad (this);
	}
		
	private void Start () {
		currentDialouges = introDialouge;
		currentString = 0;
		StartCoroutine (FadeOut());
		cooldown = cooldownLength;
		dialougeManager.Deactivate ();
		StartCoroutine (Initialise ());
	}

	private IEnumerator FadeOut(){
		yield return new WaitForEndOfFrame();
		fade.FadeToTransparent ();
	}

	private IEnumerator Initialise(){
		yield return new WaitForSeconds(initialDelay);
		dialougeManager.Activate ();
		yield return new WaitForEndOfFrame ();
		nextItem ();
		StartCoroutine( IterateDialouge ( () => {
			Deactivate();
		}));
	}

	private void Deactivate(){
		dialougeManager.Deactivate ();
		PlayerController.instance.enabled = true;
	}

	private IEnumerator IterateDialouge(System.Action onEnd){
		while (currentString < currentDialouges.Length + 1) {
			cooldown -= 0.1f;
			if (Input.GetAxis ("Submit") == 1 && cooldown <= 0) {
				currentString++;
				cooldown = cooldownLength;
				if (currentString < currentDialouges.Length) {
					nextItem ();	
				}
			}
			yield return null;
		}

		onEnd ();

	}

	private void nextItem(){
		this.dialougeManager.DisplayDialougeWithAudio (currentDialouges [currentString]);
	}
		
	public void Death(){
		StartCoroutine (DeathInitialise ());
	}

	private IEnumerator DeathInitialise(){
		yield return new WaitForSeconds(initialDelay);
		dialougeManager.Activate ();
		currentString = 0;
		currentDialouges = deathDialouge;
		cooldown = cooldownLength;
		yield return new WaitForEndOfFrame ();
		nextItem ();
		StartCoroutine( IterateDialouge ( () => {
			Deactivate();
		}));
	}

	public void End(){
		StartCoroutine (EndInitialise ());
	}

	private IEnumerator EndInitialise(){
		yield return new WaitForSeconds(initialDelay);
		dialougeManager.Activate ();
		currentString = 0;
		currentDialouges = endDialouge;
		cooldown = cooldownLength;
		yield return new WaitForEndOfFrame ();
		nextItem ();
		StartCoroutine( IterateDialouge ( () => {
			HandleEnd();
		}));
	}

	private void HandleEnd(){
		Debug.Log("game finished with score: " + ScoreCounter.instance.GetScore());
		SceneManager.LoadScene (0);
	}
}
