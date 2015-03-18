using UnityEngine;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		List<SaveObject> testSave = new List<SaveObject> (){new SaveObject("FirstRun","True")};
		List<SaveObject> testLoad = new List<SaveObject> ();
		GameFile.Save ("save.data", testSave);

		GameFile.Load ("save.data",ref testLoad);
		Debug.Log ("Loading save.data ->" + testLoad [0].key + ":" + testLoad [0].value); 
	}
	
 

	public void OpenScene(string newSceneName)
	{
		Debug.Log("Open Scene:" +  newSceneName);
		Application.LoadLevel(newSceneName);
	}
}
