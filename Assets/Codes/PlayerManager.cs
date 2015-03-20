using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerManager : MonoBehaviour {

	private int playerScore = 0;
	private int MAXSCORELENGTH = 100;
	// Use this for initialization
	void OnGUI () {
		GUIStyle myStyle = new GUIStyle(GUI.skin.textField);
		myStyle.alignment = TextAnchor.MiddleRight;
		myStyle.fontSize = 25;
		GUI.TextField (new Rect (10, 10, 100, 30), playerScore.ToString(), MAXSCORELENGTH, myStyle );
	}
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
	public void setPlayerScore(int score) {
		playerScore = score;
	}
	// Update is called once per frame
	void Update () {

	}

	public void UpdateSocre(int newScore)
	{
		GameObject.Find ("ScoreText").GetComponent<Text>().text = newScore.ToString();
	}
}
