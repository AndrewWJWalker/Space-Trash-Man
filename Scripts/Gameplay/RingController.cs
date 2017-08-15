using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class RingController : MonoBehaviour {

	[SerializeField]
	private GameObject[] rings = null;
	[SerializeField]
	private AudioClip[] sounds = null;
	[SerializeField]
	private TutorialTimer timer = null;
	[SerializeField]
	private Fade fade = null;

	private AudioSource audioSource;
	private int currentRing;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		currentRing = 0;
	}

	void OnTriggerEnter(Collider other){

		if (other.gameObject == rings[currentRing]) {
			if (currentRing == 0) {
				timer.enabled = true;
			}
			//play sound
			int soundIndex = Random.Range(0, sounds.Length-1);
			audioSource.PlayClip (sounds [soundIndex]);

			rings[currentRing].SetActive(false);

			if (currentRing < rings.Length-1) {
				currentRing++;
				rings [currentRing].SetActive (true);
			}

			if (currentRing == rings.Length - 1) {
				EndTutorial ();
			}

		}
	}

	void EndTutorial(){
		PlayerController.instance.enabled = false;
		this.GetComponent<PlayerDeath> ().enabled = false;
		double time = timer.GetTime ();
		timer.enabled = false;
		Debug.Log ("Tutorial finished with time :" + time); 
		fade.FadeToOpaque (() => {
			SceneManager.LoadScene(0);
		});
	}

}
