using UnityEngine;
using System.Collections.Generic;

public class SceneManager
{
	// Use this for initialization
	public static void TestSaveLoad () {
#if UNITY_EDITOR
 		List<SaveObject> testSave = new List<SaveObject> (){new SaveObject("FirstRun","True")};
 		GameFile.Save ("save.data", testSave);
#endif
		List<SaveObject> testLoad = new List<SaveObject> ();
		GameFile.Load ("save.data",ref testLoad);
		Debug.Log ("Loading save.data ->" + testLoad [0].key + ":" + testLoad [0].value); 
		testLoad [0].value = testLoad [0].value == "True" ? "False" : "True";
		GameFile.Save ("save.data", testLoad);

	}


	
	public static void OpenScene(string newSceneName)
	{
		Debug.Log("Open Scene:" +  newSceneName);
		Application.LoadLevel(newSceneName);
	}
}
