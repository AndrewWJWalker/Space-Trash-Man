using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Breakable : MonoBehaviour {

	[SerializeField]
	private TrashTypes type = TrashTypes.Tree;
	[SerializeField]
	private int maxLife = 10;
	[SerializeField]
	private AudioClip explosionClip = null;
	[SerializeField]
	private GameObject explosion = null;

	private TrashController trashController;
	private AudioSource audioSource;
	private int life;
	private bool isAlive;

	void Start () {
		trashController = GameObject.FindWithTag ("TrashController").GetComponent<TrashController> ();
		life = maxLife;
		isAlive = true;
		this.audioSource = this.GetComponent<AudioSource> ();
	}

	public void Hit (){
		this.life--;
		if (life <= 0 && this.isAlive) {
			this.Explode ();
		}
	}

	private void Explode(){
		isAlive = false;
		gameObject.GetComponent<MeshRenderer>().enabled = false;
		gameObject.GetComponent<Collider>().enabled = false;

		this.audioSource.clip = explosionClip;
		this.audioSource.Play ();
		GameObject explosionInstance = Instantiate(explosion,this.transform.position, this.transform.rotation);
		explosionInstance.transform.localScale = this.transform.localScale/2;

		trashController.AddTrash (GetComponent<MeshFilter> ().mesh, GetComponent<MeshRenderer> ().materials, this.transform.localScale);

		ScoreCounter.instance.AddScore (this.type);

		Destroy (this);
	}


	void OnCollisionEnter (Collision collision) {
		if (collision.collider.tag == "Bullet" && this.isAlive) {
			this.Hit ();
			audioSource.Play ();
		}
	}
}
