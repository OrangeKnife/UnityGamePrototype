using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	private int playerScore = 0;
	// Use this for initialization
	void Start () {
	
	}

	public void addPlayerScore(int score) {
		playerScore += score;
	}

	public int getPlayerScore() {
		return playerScore;
	}
	// Update is called once per frame
	void Update () {

		print ("Score: " + playerScore);
	}
}
