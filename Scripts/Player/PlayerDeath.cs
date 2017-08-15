using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VR = UnityEngine.VR;

[RequireComponent(typeof(AudioSource))]
public class PlayerDeath : MonoBehaviour {

	[SerializeField]
	private Transform mesh = null;
	[SerializeField]
	private GameObject explosion = null;
	[SerializeField]
	private Fade fadeBlack = null;
	[SerializeField]
	private AudioClip explosionSound = null;

	[SerializeField]
	private PlayerController playerController = null;
	[SerializeField]
	private Transform playerSpawn = null;


	private AudioSource audioSource;

	private void Start(){
		audioSource = GetComponent<AudioSource> ();
	}

	private void OnCollisionEnter(Collision colidee){
		StartCoroutine (this.Die ());
	}

	private IEnumerator Die(){
		ScoreCounter.instance.AddScore (-1);
		playerController.enabled = false;
		mesh.gameObject.SetActive (false);
		explosion.SetActive (true);
		audioSource.PlayClip (explosionSound);
		fadeBlack.StopAllCoroutines ();
		fadeBlack.FadeToOpaque ();
		yield return new WaitForSeconds (4.0f);
		//SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
		ResetPlayer();
	}

	private void ResetPlayer(){
		explosion.SetActive (false);
		fadeBlack.StopAllCoroutines ();
		fadeBlack.FadeToTransparent ();
		mesh.gameObject.SetActive (true);
		GameObject player = playerController.gameObject;
		player.transform.position = playerSpawn.position;
		player.transform.eulerAngles = Vector3.zero;
		PlayerEventManagerMain.instance.Death ();
		VR.InputTracking.Recenter ();
		this.transform.localPosition = new Vector3 (0, 0, 0);
	}

}
