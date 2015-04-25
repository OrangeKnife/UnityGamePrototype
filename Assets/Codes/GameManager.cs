using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;

public class GameManager : MonoBehaviour {


	public List<GameObject> PlayerTemplates;
	public List<string> AbilityTemplates;
	public Dictionary<string, AudioClip> audioAll;

	private GameObject CurrentPlayer;
	AbilityManager abManager;

	bool bGameStarted = false;
	GameSceneEvents eventHandler = null;
	List<string> AbilityNameArray = new List<string>();
	GameObject CurrentPlayerTemplate;

	//sound
	public AudioClip bgMusic;
	public AudioClip coinCollect;
	public AudioClip jump;
	public AudioClip dash;
	public AudioClip crash;
	public AudioClip warp;
	private AudioSource audioSource;
	private AudioSource audioSource_PlayOnce;
	private AudioClip _audioClip;
	private bool bAudioAvailable = false;//turn on music when save file is loaded

	void Start () {
		//RespawnPlayer();

		//initial audio source
		audioSource=gameObject.AddComponent<AudioSource>();
		audioSource.loop = true;
		audioSource.mute = !bAudioAvailable;

		audioSource_PlayOnce=gameObject.AddComponent<AudioSource>();
		audioSource_PlayOnce.loop = false;
		audioSource_PlayOnce.mute = !bAudioAvailable;
	}

	public void StartGame()
	{
		bGameStarted = true;
		GetComponent<LevelGenerator> ().enabled = bGameStarted;


	}

	public void EndGame()
	{
		bGameStarted = false;
		GetComponent<LevelGenerator> ().enabled = bGameStarted;
	}

	public void SetAudioAvailable(bool bAvailable)
	{
		if (audioSource != null)
			audioSource.mute = !bAvailable;
		else
			bAudioAvailable = bAvailable;

		if (audioSource_PlayOnce != null)
			audioSource_PlayOnce.mute = !bAvailable;


	}

	public void PlayBackground() {
		if (bAudioAvailable) {	
			audioSource.clip = bgMusic;
			audioSource.Play ();
		}
	}

	public void CleanUpAbilityNames()
	{
		AbilityNameArray.Clear ();
	}

	public bool SetCurrentPlayerTemplateByIdx(int idx)
	{
		if (PlayerTemplates.Count > 0) {
			CurrentPlayerTemplate = PlayerTemplates [idx];
			return true;
		} else 
			return false;
	}

	public bool RemoveAbilityByIndex(int idx)
	{
		if (AbilityTemplates.Count > 0 && idx > -1) {
			if(AbilityNameArray.IndexOf(AbilityTemplates [idx]) > -1)
			{
				AbilityNameArray.Remove (AbilityTemplates [idx]);
				return true;
			}
			return false;
		} else
			return false;
	}

	public bool AddAbilityByIndex(int idx)
	{
		if (AbilityTemplates.Count > 0 && idx > -1) {
			AbilityNameArray.Add (AbilityTemplates [idx]);
			return true;
		} else
			return false;
	}

	public bool SetActiveAbilityByIndex(int idx)
	{
		if (AbilityTemplates.Count > 0 && idx > -1) {
			AbilityNameArray.Add (AbilityTemplates [idx]);
			return true;
		} else
			return false;
	}

	// Update is called once per frame
	void Update () {

	}

	void Awake(){
		addAudioDictionary ();
		DontDestroyOnLoad(gameObject);
	}

	private void addAudioDictionary() {
		audioAll = new Dictionary<string, AudioClip>();
		audioAll.Add ("coin", coinCollect);
		audioAll.Add ("jump", jump);
		audioAll.Add ("dash", dash);
		audioAll.Add ("crash", crash);
		audioAll.Add ("warp", warp);
	}
	public void RespawnPlayer()
	{
		if (CurrentPlayer != null)
		{
			Destroy(CurrentPlayer);
		}

		if(eventHandler == null)
			eventHandler = GameObject.Find ("eventHandler").GetComponent<GameSceneEvents>();

		eventHandler.CleanUpAbilityUISlots ();


		CurrentPlayer = Instantiate(CurrentPlayerTemplate);
		//AbilityNameArray [0] = "abi1";
		abManager = CurrentPlayer.GetComponent<AbilityManager>();
		for (int i = 0; i < AbilityNameArray.Count; ++i)
		{
			print ("add ability: "+AbilityNameArray[i]);
			abManager.addAbility(AbilityNameArray[i], eventHandler.CreateAbilityUISlot(new Vector3(15 + i*65+ i*15,0,0)));
		}

		GetComponent<LevelGenerator>().InitLevel();

		if(eventHandler)
			eventHandler.onPlayerRespawn ();
	}

	public GameObject GetCurrentPlayer()
	{
		return CurrentPlayer;
	}
	public void restorePurchase()
	{
		SoomlaStore.RestoreTransactions ();
	}
	public void playSound(string type,bool bLooping = false, float pitch = 1)
	{
		/*if (type == "coin")
			_audioClip = coinCollect;
		else if (type == "jump")
			_audioClip = jump;
		else if (type == "dash")
			_audioClip = dash;
		else if (type == "crash")
			_audioClip = crash;
		else if (type == "warp")
			_audioClip = warp;*/
		audioSource_PlayOnce.clip = audioAll[type];
		audioSource_PlayOnce.loop = bLooping;
		audioSource_PlayOnce.pitch = pitch;
		audioSource_PlayOnce.Play ();
	}

}
