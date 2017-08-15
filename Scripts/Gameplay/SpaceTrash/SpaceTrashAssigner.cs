using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceTrashAssigner : MonoBehaviour {

	[SerializeField]
	private GameObject[] objects = null;

	public int GetLength(){
		return objects.Length;
	}

	public GameObject GetObject(int index){
		return objects [index];
	}

}
