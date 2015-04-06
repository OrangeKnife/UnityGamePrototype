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
	}

	public GameObject GetCurrentPlayer()
	{
		return CurrentPlayer;
	}
}
