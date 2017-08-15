using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	//main


	//tutorial
	[SerializeField]
	private GameObject rings;
	[SerializeField]
	private RingController ringcontroller;


	void Start () {
		GameState state = GlobalSettings.gameState;
		switch (state) {
		case GameState.Main:
				
			break;
		case GameState.Tutorial:
			rings.SetActive (true);
			ringcontroller.enabled = true;
			break;
		}
	}

}
