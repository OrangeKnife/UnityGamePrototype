using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Soomla.Store;

[RequireComponent(typeof(AudioSource))]
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
	private AudioSource bgAudioSource;
	private AudioClip _audioClip;
	private bool bAudioAvailable = false;//turn on music when save file is loaded

	private static GameManager _instance;
	
	public static GameManager instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<GameManager>();
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}



	void Start () {
		//RespawnPlayer();


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
		if (bgAudioSource != null)
			bgAudioSource.mute = !bAvailable;

		bAudioAvailable = bAvailable;
		PlayBackgroundMusic ();

	}

	public void PlayBackgroundMusic() {
		if (bAudioAvailable && !bgAudioSource.isPlaying) {	
			bgAudioSource.Play ();
		}
		//print ("bAudioAvailable: "+bAudioAvailable);
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

	public bool RemoveAllAbilities()
	{
		AbilityNameArray.Clear ();
		return true;
	}

	public bool RemoveAbilityById(int abiId)
	{
		if (AbilityTemplates.Count > 0 && abiId > -1 && abiId < AbilityTemplates.Count) {
			if(AbilityNameArray.IndexOf(AbilityTemplates [abiId]) > -1)
			{
				AbilityNameArray.Remove (AbilityTemplates [abiId]);
				return true;
			}
			return false;
		} else
			return false;
	}

	public bool AddAbilityById(int abiId)
	{
		if (AbilityTemplates.Count > 0 && abiId > -1 && abiId < AbilityTemplates.Count) {
			AbilityNameArray.Add (AbilityTemplates [abiId]);
			return true;
		} else
			return false;
	}
	

	// Update is called once per frame
	void Update () {

	}

	void Awake(){
		if(_instance == null)
		{
			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			if(this != _instance)
			{
				Destroy(this.gameObject);
				return;
			}
		}

		addAudioDictionary ();
		DontDestroyOnLoad(gameObject);

		//initial audio source
		bgAudioSource=gameObject.AddComponent<AudioSource>();
		bgAudioSource.loop = true;
		bgAudioSource.mute = !bAudioAvailable;
		bgAudioSource.clip = bgMusic;
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
			abManager.addAbility(AbilityNameArray[i], eventHandler.CreateAbilityUISlot(15f,15f,i));
		}

		GetComponent<LevelGenerator>().InitLevel();

		if(eventHandler)
			eventHandler.onPlayerRespawn ();

		PlayBackgroundMusic ();
	}

	public GameObject GetCurrentPlayer()
	{
		return CurrentPlayer;
	}

	public void restorePurchase()
	{
		SoomlaStore.RestoreTransactions ();
	}

	public void playSound (string type, bool isStopBackground = false)
	{
		if (bAudioAvailable) 
		{	
			AudioSource.PlayClipAtPoint (audioAll [type], new Vector3 (5, 1, 2));
		}
		if (isStopBackground) 
		{
			bgAudioSource.Stop();
		}
		print ("bAudioAvailable: "+bAudioAvailable);
	}
	
}
