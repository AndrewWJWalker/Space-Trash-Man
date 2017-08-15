using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicHandler : MonoBehaviour {

	[SerializeField]
	private AudioClip[] clips = null;

	[SerializeField]
	private bool isRandom = false;

	private AudioSource audioSource;
	private int index;

	void Start () {
		audioSource = gameObject.GetComponent<AudioSource> ();
		if (isRandom) {
			index = UnityEngine.Random.Range (0, clips.Length - 1);
		} else {
			index = 0;
		}
		StartCoroutine (
			audioSource.PlayClip (clips [index], () => {
				NextClip ();
			}
			)
		);
	}
	
	public void NextClip(){
		if (isRandom) {
			index = UnityEngine.Random.Range (0, clips.Length - 1);
		} else {
			index++;
		}

		if (index >= clips.Length) {
			index = 0;
		}

		StartCoroutine (
			audioSource.PlayClip (clips [index], () => {
				NextClip ();
			}
			)
		); 
	}



}
