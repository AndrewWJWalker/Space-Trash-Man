using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashController : MonoBehaviour {

	[SerializeField]
	private GameObject[] trash = null;

	private int index;

	void Start () {
		index = 0;
	}
	
	public void AddTrash (Mesh mesh, Material[] materials, Vector3 scale) {
		if (index < trash.Length) {
			GameObject newTrash = trash [index];

			newTrash.AddComponent<MeshFilter> ().mesh = mesh;
			newTrash.AddComponent<MeshRenderer> ().materials = materials;
			newTrash.transform.localScale = scale / 100;


			index++;
		}
	}

}
