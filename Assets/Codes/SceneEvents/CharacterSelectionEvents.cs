using UnityEngine;
using System.Collections;

public class CharacterSelectionEvents : MonoBehaviour {

	CharacterSelector selector;

	void Start () {
		selector = GameObject.Find ("CharacterSelector").GetComponent<CharacterSelector>();
	}
	// Update is called once per frame
	void Update () {
	
	}

	public void OnBackButtonClick()
	{
		selector.SaveSelection ();
		SceneManager.OpenScene("MainMenu");
	}

	public void OnGoButtonClick()
	{
		selector.SaveSelection ();
		SceneManager.OpenScene("TestSceneBank");
	}
}
