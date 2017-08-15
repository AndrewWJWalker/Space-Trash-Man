using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeManager : MonoBehaviour {

	[SerializeField]
	private GameObject canvas = null;
	[SerializeField]
	private Text textUI = null;
	[SerializeField]
	private float delay = 0.1f;
	[SerializeField]
	private float pitch = 1.2f;
	[SerializeField]
	private AudioClip[] letterClips  = null;
	[SerializeField]
	private string[] letters  = null;
	[SerializeField]
	private Sprite mouthClosed = null;
	[SerializeField]
	private Sprite mouthOpen = null;
	[SerializeField]
	private Image faceImage = null;

	private Dictionary<string, AudioSource> letterDictionary;
	private string currentString;

	private void Start(){
		letterDictionary = new Dictionary<string, AudioSource> ();
		for (int loop = 0; loop < letterClips.Length; loop++) {
			AudioSource loopSource = this.gameObject.AddComponent<AudioSource> ();
			loopSource.clip = letterClips [loop];
			loopSource.pitch = this.pitch;
			letterDictionary.Add (letters [loop], loopSource);
		}
	}

	public void Activate(){
		canvas.SetActive (true);
	}

	public void Deactivate(){
		canvas.SetActive (false);
		textUI.text = "";
	}

	public void DisplayDialouge(string dialouge){
		textUI.text = dialouge;
	}

	public void DisplayDialougeWithAudio(string dialouge){
		//using a delay, display one character at a time
		//play character noise

		StopAllCoroutines ();
		currentString = "";
		char[] dialougeArray = dialouge.ToCharArray ();
		StartCoroutine (StringTicker(dialougeArray));

	}

	private IEnumerator StringTicker(char[] dialougeArray){
		foreach(char loopChar in dialougeArray){
			currentString = currentString + loopChar;
			DisplayDialouge (currentString);
			PlayLetterSound (loopChar);
			SwitchImage ();
			yield return new WaitForSeconds (delay);
		}
		faceImage.sprite = mouthClosed;
	}

	private void PlayLetterSound(char letter){
		letter = char.ToLower (letter);
		if (letterDictionary.ContainsKey(letter.ToString())){
			AudioSource audioSource = letterDictionary [letter.ToString()];
			audioSource.Play();
		}
	}

	private void SwitchImage(){
		if (faceImage.sprite == mouthOpen) {
			faceImage.sprite = mouthClosed;
		} else {
			faceImage.sprite = mouthOpen;
		}
	}
}
