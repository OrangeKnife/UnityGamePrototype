using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public GameObject PlayerTemplate;
	public List<string> AbilityNameArray;

	private GameObject CurrentPlayer;
	AbilityManager abManager;

	// Use this for initialization
	void Start () {
		RespawnPlayer();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void RespawnPlayer()
	{
		if (CurrentPlayer != null)
		{
			Destroy(CurrentPlayer);
		}

		CurrentPlayer = Instantiate(PlayerTemplate);
		abManager = CurrentPlayer.GetComponent<AbilityManager>();
		for (int i = 0; i < AbilityNameArray.Count; ++i)
		{
			abManager.addAbility(AbilityNameArray[i]);
		}

		GetComponent<LevelGenerator>().InitLevel();
	}

	public GameObject GetCurrentPlayer()
	{
		return CurrentPlayer;
	}
}
