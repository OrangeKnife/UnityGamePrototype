using UnityEngine;
using System.Collections;

public class CharacterSelectionEvents : MonoBehaviour {

	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnBackButtonClick()
	{
		SceneManager.OpenScene("MainMenu");
	}

	public void OnGoButtonClick()
	{
		SceneManager.OpenScene("TestSceneBank");
	}
}
