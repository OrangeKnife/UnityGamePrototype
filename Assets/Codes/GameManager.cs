using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject Player;

	// Use this for initialization
	void Start () {
		RespawnPlayer();
	}
	
	// Update is called once per frame
	void Update () {

	}



	public void RespawnPlayer()
	{
		//Instantiate(Player);
		GetComponent<LevelGenerator>().InitLevel();
	}
}
