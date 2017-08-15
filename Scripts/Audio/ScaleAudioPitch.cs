using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleAudioPitch : MonoBehaviour {

	[SerializeField]
	private PlayerController player =  null;
	[SerializeField]
	private float minPitch = 0.7f;

	private AudioSource _audioSource;
	private float _pitch;

	// Use this for initialization
	void Start () {
		_audioSource = this.gameObject.GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		_audioSource.pitch = minPitch + player.GetVelocityAsPercentage ();
	}
}
