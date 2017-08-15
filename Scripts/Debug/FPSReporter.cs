using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSReporter : MonoBehaviour {
	float deltaTime = 0.0f;
	public TextMesh textObject;
	void Update()
	{
		deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

		float msec = deltaTime * 1000.0f;
		float fps = 1.0f / deltaTime;
		string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);
		textObject.text = text;
	}
}
