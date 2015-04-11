using UnityEngine;
using System.Collections;
public class PlayerManager : MonoBehaviour {

	public float Score2GoldRatio = 1f;

	private float CoinMultiplier = 1.0f;
	private int playerScore = 0;
	private int MAXSCORELENGTH = 100;

	private SaveObject mysave;

	private int coins = 0;
	// Use this for initialization

	GameSceneEvents eventHandler;
	void OnGUI () {
//		GUIStyle myStyle = new GUIStyle(GUI.skin.textField);
//		myStyle.alignment = TextAnchor.MiddleRight;
//		myStyle.fontSize = 25;
//		GUI.TextField (new Rect (10, 10, 100, 30), playerScore.ToString(), MAXSCORELENGTH, myStyle );
	}
	void Start () {
		eventHandler = GameObject.Find ("eventHandler").GetComponent<GameSceneEvents>();

		GameFile.Load ("save.data", ref mysave);
		coins = mysave.playerGold;
		eventHandler.UpdateUICoins (coins);
	}

	public void addPlayerScore(int score) {
		playerScore += score;
		eventHandler.UpdateUISocre (playerScore);

	}
	public void addCoin(int coin) {
		coins += (int)(coin * CoinMultiplier);
		print ("coins : " + coins);
		mysave.playerGold = coins;
		GameFile.Save ("save.data", mysave);
		eventHandler.UpdateUICoins (coins);
	}

	public int getPlayerScore() {
		return playerScore;
	}
	public void setPlayerScore(int score) {
		playerScore = score;
		eventHandler.UpdateUISocre (playerScore);
	}

	public void setCoinMultiplier(float val)
	{
		CoinMultiplier = val;
	}
	public float getCoinMultiplier()
	{
		return CoinMultiplier;
	}

 

	// Update is called once per frame
	void Update () {

	}


}
