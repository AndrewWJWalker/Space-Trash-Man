using UnityEngine;
using System.Collections;

public class AttractParticles : MonoBehaviour {

	private GameObject _attractorTransform = null;

	private ParticleSystem _particleSystem;
	private ParticleSystem.Particle[] _particles = new ParticleSystem.Particle[1000];



	public void Start ()
	{
		_particleSystem = GetComponent<ParticleSystem> ();
		_attractorTransform = GameObject.FindGameObjectWithTag ("ParticleTarget");
	}

	public void LateUpdate()
	{
		if (_particleSystem.isPlaying) {
			int length = _particleSystem.GetParticles (_particles);
			Vector3 attractorPosition =   _attractorTransform.transform.position;//this.transform.position - _attractorTransform.position;

			for (int i=0; i < length; i++) {
				_particles [i].position = _particles [i].position + (attractorPosition - _particles [i].position) / (_particles [i].remainingLifetime) * Time.deltaTime;
				//_particles [i].velocity = Vector3.Lerp (_particles [i].velocity, (attractorPosition - _particles [i].position).normalized, 0.5f);
				//_particles[i].position = Vector3.MoveTowards(_particles[i].position, attractorPosition, speed * Time.deltaTime);
			}
			_particleSystem.SetParticles (_particles, length);
		}

	}
}