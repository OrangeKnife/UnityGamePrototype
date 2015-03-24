using UnityEngine;
using System.Collections;
public class PlayerManager : MonoBehaviour {

	private int playerScore = 0;
	private int MAXSCORELENGTH = 100;
	// Use this for initialization

	GameSceneEvents eventHandler;
	void OnGUI () {
		GUIStyle myStyle = new GUIStyle(GUI.skin.textField);
		myStyle.alignment = TextAnchor.MiddleRight;
		myStyle.fontSize = 25;
		GUI.TextField (new Rect (10, 10, 100, 30), playerScore.ToString(), MAXSCORELENGTH, myStyle );
	}
	void Start () {
		eventHandler = GameObject.Find ("eventHandler").GetComponent<GameSceneEvents>();
	}

	public void addPlayerScore(int score) {
		playerScore += score;
		eventHandler.UpdateUISocre (playerScore);
	}

	public void resetPlayerScore(){
		playerScore = 0;
		eventHandler.UpdateUISocre (playerScore);
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


}
