using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using Soomla.Store;
public class MainMenuEvents : MonoBehaviour {
	public GameObject OptionPanel;

	private SaveObject mysave;// = new SaveObject();

	 

	private GameManager gameMgr;
	void Start () {

		if(GameFile.Load ("save.data", ref mysave))
			GameObject.Find ("WelcomeText").GetComponent<Text> ().enabled = mysave.firstRun == "True";
		else
			mysave = new SaveObject("False",0);

		gameMgr = GameObject.Find ("GameManager").GetComponent<GameManager>() ;
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

		SceneManager.OpenScene ("CharacterSelection");
	}

	public void overSaveData()
	{
		//TODO remove when realease !
		mysave = new SaveObject("False",0);
		GameFile.Save ("save.data",mysave);
#if UNITY_EDITOR
		PlayerPrefs.DeleteAll ();
#endif

#if UNITY_IOS || UNITY_ANDROID

#endif

	}

	public void OnTestShopButtonClick(string sceneName)
	{
		SceneManager.OpenScene (sceneName);
	}

	public void OnRestoreButtonClick()
	{
		gameMgr.restorePurchase ();
	}

	public void OnOptionButtonClick()
	{
		OptionPanel.SetActive (true);
	}

	public void OnCloseOptionButtonClick()
	{
		OptionPanel.SetActive (false);
	}
























}
