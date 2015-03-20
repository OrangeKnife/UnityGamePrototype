using UnityEngine;
using System.Collections.Generic;

public class SceneManager
{
	// Use this for initialization
 
	
	public static void OpenScene(string newSceneName)
	{
		Debug.Log("Open Scene:" +  newSceneName);
		Application.LoadLevel(newSceneName);
	}
}
