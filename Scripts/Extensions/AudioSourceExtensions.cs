using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class AudioSourceExtensions {

	// this Object myObject --- denotes an extension method to the class Object

	public static void PlayClip (this AudioSource audioSource, AudioClip clip){
		audioSource.clip = clip;
		audioSource.Play ();
	}

	public static IEnumerator PlayClip(this AudioSource audioSource, AudioClip clip, Action onComplete){
		audioSource.PlayClip (clip);

		while (audioSource.isPlaying) {
			yield return null;
		}

		onComplete ();
	}

}
