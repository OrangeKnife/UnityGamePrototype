using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject PlayerTemplate;
	private GameObject CurrentPlayer;

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
		GetComponent<LevelGenerator>().InitLevel();
	}

	public GameObject GetCurrentPlayer()
	{
		return CurrentPlayer;
	}
}
