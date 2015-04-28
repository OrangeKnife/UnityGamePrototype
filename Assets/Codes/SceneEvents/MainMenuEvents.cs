using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using Soomla.Store;
public class MainMenuEvents : MonoBehaviour {
	public GameObject OptionPanel;
	public Toggle MusicAndSoundToggle;


	private SaveObject mysave;// = new SaveObject();

	 

	private GameManager gameMgr;

	ShopEventHandler shopEvents;

	void Start () {
		gameMgr = GameObject.Find ("GameManager").GetComponent<GameManager>() ;

		try{
			if(GameFile.Load ("save.data", ref mysave))
				GameObject.Find ("WelcomeText").GetComponent<Text> ().enabled = mysave.firstRun == "True";
			else
				mysave = new SaveObject("False");

			MusicAndSoundToggle.isOn = mysave.optionMusic;
			gameMgr.SetAudioAvailable (mysave.optionMusic);
		}
		catch(System.Exception)
		{
			Debug.Log ("save.data loading error");
		}

		
		shopEvents = new ShopEventHandler ();

		
		//shop
		if(!SoomlaStore.Initialized)
			SoomlaStore.Initialize(new ShopAssets());

		if (StoreInventory.GetItemBalance ("freechar0") == 0)
			StoreInventory.GiveItem ("freechar0", 1);

		if(StoreInventory.GetItemBalance("noability") == 0)
			StoreInventory.GiveItem ("noability", 1);


	}

	void OnDestroy()
	{
		shopEvents.RemoveCallbacks ();
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
		mysave = new SaveObject("False");
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
		Utils.addLog ("gameMgr.restorePurchase()");
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

	public void onOptionMusicChanged(Boolean wtf)
	{
		if (mysave != null && gameMgr != null) {
			mysave.optionMusic = MusicAndSoundToggle.isOn;
			gameMgr.SetAudioAvailable (mysave.optionMusic);
			GameFile.Save ("save.data", mysave);
		}
	}




















}
