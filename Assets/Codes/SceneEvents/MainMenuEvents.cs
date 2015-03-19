using UnityEngine;
using System.Collections;

public class MainMenuEvents : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnStartButtonClick()
	{
		Debug.Log ("Start");
		SceneManager.OpenScene("GameScene");
	}
}
