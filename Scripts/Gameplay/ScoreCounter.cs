using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour {

	[SerializeField]
	private TrashTypes desiredType = TrashTypes.Tree;
	[SerializeField]
	private Text text = null;
	[SerializeField]
	private int itemMax = 15;

	public static ScoreCounter instance;

	private int score;
	private int amountCollected;

	void Start(){
		score = 0;
	}

	void Awake(){
		if (instance != null) {
			GameObject.Destroy (instance);
		} else {
			instance = this;
		}
	}

	public void AddScore(int points){
		score += points;
	}

	public void AddScore(TrashTypes type){
		if (type == desiredType) {
			score++;
		} else {
			score--;
		}
		amountCollected++;
		text.text = "SCORE: " + score;
		if (amountCollected >= itemMax) {
			EndGame ();
		}
	}

	public int GetScore(){
		return score;
	}

	private void EndGame(){
		PlayerEventManagerMain.instance.End ();
	}
}
