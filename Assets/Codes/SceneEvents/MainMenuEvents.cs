using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
public class MainMenuEvents : MonoBehaviour {

	private SaveObject mysave;// = new SaveObject();
	// Use this for initialization
	void Start () {
		if(GameFile.Load ("save.data", ref mysave))
			GameObject.Find ("WelcomeText").GetComponent<Text> ().enabled = mysave.firstRun == "True";
		else
			mysave = new SaveObject();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake()
	{
		Environment.SetEnvironmentVariable("MONO_REFLECTION_SERIALIZER", "yes");
	}

	public void OnStartButtonClick()
	{
		  
		mysave.firstRun = "False";
		GameFile.Save ("save.data", mysave);

			SceneManager.OpenScene ("GameScene");
	}
}
