using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class MainMenuEvents : MonoBehaviour {

	private List<SaveObject> mysave = new List<SaveObject>();
	// Use this for initialization
	void Start () {
		GameFile.Load ("save.data", ref mysave);
		GameObject.Find ("WelcomeText").GetComponent<Text> ().enabled = mysave[0].value == "True";
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnStartButtonClick()
	{
		mysave [0].value = "False";
		GameFile.Save ("save.data",  mysave);
		SceneManager.OpenScene("GameScene");
	}
}
