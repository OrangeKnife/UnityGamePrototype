using UnityEngine;
using System.Collections;
public class PlayerManager : MonoBehaviour {

	public float Score2GoldRatio = 1f;

	private int playerScore = 0;
	private int MAXSCORELENGTH = 100;

	private SaveObject mysave;

	int gainedGold = 0;
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

		GameFile.Load ("save.data", ref mysave);
		gainedGold = mysave.playerGold;
		eventHandler.UpdateUIGold (gainedGold);
	}

	public void addPlayerScore(int score) {
		playerScore += score;
		eventHandler.UpdateUISocre (playerScore);

	}

	public int getPlayerScore() {
		return playerScore;
	}
	public void setPlayerScore(int score) {
		playerScore = score;
		eventHandler.UpdateUISocre (playerScore);
	}

	public void addGold(int goldNum)
	{
		gainedGold += goldNum;

		
		mysave.playerGold = gainedGold;
		GameFile.Save ("save.data", mysave);
		eventHandler.UpdateUIGold (gainedGold);
	}

	public int getGainedGold()
	{
		return gainedGold;
	}


	// Update is called once per frame
	void Update () {

	}


}
