using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour {

	private int playerScore = 0;
	// Use this for initialization
	void Start () {
	
	}

	public void addPlayerScore(int score) {
		playerScore += score;
		UpdateSocre (playerScore);
	}

	public void resetPlayerScore(){
		playerScore = 0;
		UpdateSocre (playerScore);
	}

	public int getPlayerScore() {
		return playerScore;
	}
	// Update is called once per frame
	void Update () {

	}

	public void UpdateSocre(int newScore)
	{
		GameObject.Find ("ScoreText").GetComponent<Text>().text = newScore.ToString();
	}
}
