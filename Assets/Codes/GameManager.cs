using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject CurrentPlayerTemplate;
	public List<string> AbilityNameArray;
	public string CurrentActiveAbility;
	public List<GameObject> PlayerTemplates;

	private GameObject CurrentPlayer;
	AbilityManager abManager;

	bool bGameStarted = false;
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

		CurrentPlayer = Instantiate(CurrentPlayerTemplate);
		//AbilityNameArray [0] = "abi1";
		abManager = CurrentPlayer.GetComponent<AbilityManager>();
		for (int i = 0; i < AbilityNameArray.Count; ++i)
		{
			print ("add ability: "+AbilityNameArray[i]);
			abManager.addAbility(AbilityNameArray[i]);
		}

		GetComponent<LevelGenerator>().InitLevel();
	}

	public GameObject GetCurrentPlayer()
	{
		return CurrentPlayer;
	}
}
