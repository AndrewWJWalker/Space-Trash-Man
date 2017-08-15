using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTrash : MonoBehaviour {

	//assign random mesh
	//get type of object

	//giving each object a reference to a specific assigner, so i can draw from different object pools
	//for different orbits, if i want
	[SerializeField]
	SpaceTrashAssigner assigner = null;

	private TrashTypes type;

	void Start () {
		int index = UnityEngine.Random.Range (0, assigner.GetLength ());
		Instantiate (assigner.GetObject (index), this.transform);
	}
}
