using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour {

	[SerializeField]
	private float delay = 0.1f;
	[SerializeField]
	private float step = 0.1f;

	private MeshRenderer mesh;
	private float alpha;

	// Use this for initialization
	void Start () {
		mesh = this.GetComponent<MeshRenderer> ();
		alpha = mesh.material.color.a;
		if (alpha > 1) {
			alpha = 1;
		}
		ReverseNormals (this.gameObject);
	}

	public void FadeToOpaque(){
		StartCoroutine (this.FadeIn ());
	}

	public void FadeToOpaque(System.Action newAction){
		StartCoroutine(FadeIn(newAction));
	}

	private IEnumerator FadeIn(){
		while (alpha < 1) {
			alpha = alpha + step;
			mesh.material.color = new Color (0,0,0,alpha);
			yield return new WaitForSeconds (delay);
		}
	}

	private IEnumerator FadeIn(System.Action onEnd){
		while (alpha < 1) {
			alpha = alpha + step;
			mesh.material.color = new Color (0,0,0,alpha);
			yield return new WaitForSeconds (delay);
		}
		onEnd ();
	}

	public void FadeToTransparent(){
		StartCoroutine (this.FadeOut ());
	}

	public void FadeToTransparent(System.Action newAction){
		StartCoroutine(FadeOut(newAction));
	}

	private IEnumerator FadeOut(){
		while (alpha > 0) {
			alpha = alpha - step;
			mesh.material.color = new Color (0,0,0,alpha);
			yield return new WaitForSeconds (delay);
		}
	}

	private IEnumerator FadeOut(System.Action onEnd){
		while (alpha > 0) {
			alpha = alpha - step;
			mesh.material.color = new Color (0,0,0,alpha);
			yield return new WaitForSeconds (delay);
		}
		onEnd ();
	}

	private static void ReverseNormals(GameObject gameObject){
		// Renders interior of the overlay instead of exterior.
		// Included for ease-of-use. 
		// Public so you can use it, too.
		MeshFilter filter = gameObject.GetComponent(typeof(MeshFilter)) as MeshFilter;
		if(filter != null){
			Mesh mesh = filter.mesh;
			Vector3[] normals = mesh.normals;
			for(int i = 0; i < normals.Length; i++)
				normals[i] = -normals[i];
			mesh.normals = normals;

			for(int m = 0; m < mesh.subMeshCount; m++){
				int[] triangles = mesh.GetTriangles(m);
				for(int i = 0; i < triangles.Length; i += 3){
					int temp = triangles[i + 0];
					triangles[i + 0] = triangles[i + 1];
					triangles[i + 1] = temp;
				}
				mesh.SetTriangles(triangles, m);
			}
		}
	}

	public void SetStep(float step){
		this.step = step;
	}

	public void SetDelay(float delay){
		this.delay = delay;
	}

}
