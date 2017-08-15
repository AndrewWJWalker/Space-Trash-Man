using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class TutorialTimer : MonoBehaviour {

	[SerializeField]
	private GameObject canvas;
	[SerializeField]
	private Text text = null;

	private double time;
	private double roundedTime;

	void Start () {
		time = 0;
	}

	void Awake(){
		canvas.SetActive (true);
	}

	void Update () {
		time += Time.deltaTime;
		roundedTime = Math.Round (time, 2);
		text.text = "" + roundedTime;
	}

	public double GetTime(){
		return roundedTime;
	}
}
