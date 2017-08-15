using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FireBullets : MonoBehaviour {

	[SerializeField]
	private Transform leftHand = null;
	[SerializeField]
	private Transform rightHand = null;
	[SerializeField]
	private GameObject bulletPrefab = null;
	[SerializeField]
	private float bulletSpeed = 10.0f;
	[SerializeField]
	private float bulletLife = 2.0f;
	[SerializeField]
	private AudioClip shootClip = null;
	[SerializeField]
	private float coolDownLength = 0.1f;

	private AudioSource audioSource;
	private float leftInput;
	private float rightInput;
	private float coolDown;

	//haptics
	[SerializeField]
	private int hapticLength = 10;
	[SerializeField]
	private int hapticStrength = 55;

	private OVRHapticsClip haptic;


	// Use this for initialization
	void Start () {

		audioSource = this.GetComponent<AudioSource> ();
		this.audioSource.clip = this.shootClip;
		coolDown = 0;

		haptic = new OVRHapticsClip(hapticLength);

		for (int loop = 0; loop < hapticLength; loop++) {
			//explicity convert int to byte
			if ((hapticStrength - (loop * 10)) < 0) {
				haptic.Samples [loop] = (byte)0;
			} else {
				haptic.Samples [loop] = (byte)(hapticStrength - (loop * 10));
			}
		}

		haptic = new OVRHapticsClip (haptic.Samples, haptic.Samples.Length);

	}
	
	// Update is called once per frame
	void Update () {
		leftInput = Input.GetAxis ("FireLeft");
		rightInput = Input.GetAxis ("FireRight");

		coolDown -= Time.deltaTime;

		if (coolDown <= 0) {
			if (leftInput > 0) {
				fireBullet (leftHand, "left");
			}
			if (rightInput > 0) {
				fireBullet (rightHand, "right");
			}
		}

	}

	private void fireBullet(Transform hand, string handID){
		coolDown = coolDownLength;

		//instantiate prefab bullet 
		GameObject bullet = Object.Instantiate (bulletPrefab, hand.position, hand.rotation);
		bullet.tag = "Bullet";

		Physics.IgnoreCollision (this.GetComponent<Collider> (), bullet.GetComponent<Collider> ());

		//turn gravity off
		bullet.GetComponent<Rigidbody>().useGravity = false;
		//add velocity
		bullet.GetComponent<Rigidbody> ().velocity = bullet.transform.forward * bulletSpeed;

		//destroy after it's lifetime
		Destroy (bullet, bulletLife);

		//sound
		this.audioSource.Play();

		//haptics
		var channel = OVRHaptics.RightChannel;
		if (handID == "left") {
			channel = OVRHaptics.LeftChannel;
		}
		channel.Preempt (haptic);
	}
}
