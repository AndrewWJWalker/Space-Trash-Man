using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour 
{
	public static GlobalSettings instance;

	public static PlayerControlStates controlState;

	public static GameState gameState; 

	void Awake ()   
	{
		if (instance == null)
		{
			DontDestroyOnLoad(gameObject);
			instance = this;
			controlState = PlayerControlStates.Joystick;
		}
		else if (instance != this)
		{
			Destroy (gameObject);
		}
			
	}
}

public enum GameState{
	Main,
	Tutorial
}