using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {


	public List<GameObject> PlayerTemplates;
	public List<string> AbilityTemplates;

	private GameObject CurrentPlayer;
	AbilityManager abManager;

	bool bGameStarted = false;
	GameSceneEvents eventHandler = null;
	List<string> AbilityNameArray = new List<string>();
	GameObject CurrentPlayerTemplate;

	//sound
	public AudioClip coinCollect;
	public AudioClip jump;
	public AudioClip dash;
	public AudioClip crash;
	public AudioClip warp;
	private AudioSource audioSource;
	private AudioClip _audioClip;

	void Start () {
		//RespawnPlayer();
	}

	public void StartGame()
	{
		bGameStarted = true;
		GetComponent<LevelGenerator> ().enabled = bGameStarted;

		audioSource=gameObject.AddComponent<AudioSource>();
	}

	public void EndGame()
	{
		bGameStarted = false;
		GetComponent<LevelGenerator> ().enabled = bGameStarted;
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
		DontDestroyOnLoad(gameObject);
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
			abManager.addAbility(AbilityNameArray[i], eventHandler.CreateAbilityUISlot(new Vector3(-65 + i*65+ i*5,0,0)));
		}

		GetComponent<LevelGenerator>().InitLevel();

		if(eventHandler)
			eventHandler.onPlayerRespawn ();
	}

	public GameObject GetCurrentPlayer()
	{
		return CurrentPlayer;
	}

	public void playSound(string type,bool bLooping = false)
	{
		if (type == "coin")
			_audioClip = coinCollect;
		else if (type == "jump")
			_audioClip = jump;
		else if (type == "dash")
			_audioClip = dash;
		else if (type == "crash")
			_audioClip = crash;
		else if (type == "warp")
			_audioClip = warp;
		
		audioSource.clip = _audioClip;
		audioSource.loop = bLooping;
		audioSource.Play ();
	}

}
