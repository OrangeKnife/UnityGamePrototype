using UnityEngine;
using System.Collections;
using Soomla.Store;
public class PlayerManager : MonoBehaviour {

	public float Score2GoldRatio = 1f;

	private float CoinMultiplier = 1.0f;
	private float ScoreMultiplier = 1.0f;
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

		coins = StoreInventory.GetItemBalance(ShopAssets.COIN_CURRENCY_ITEM_ID);
		eventHandler.UpdateUICoins (coins);
	}

	public void addPlayerScore(int score) {
		playerScore += score;
		eventHandler.UpdateUISocre (playerScore);

	}
	public void addCoin(int coin) {
		coins += (int)(coin * CoinMultiplier);
		print ("coins : " + coins);
		StoreInventory.GiveItem ("coin1", coin);
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

	public void setScoreMultiplier(float scoreMulti) {
		ScoreMultiplier = scoreMulti;
	}
	
	public float getScoreMultiplier() {
		return ScoreMultiplier;
	}

	// Update is called once per frame
	void Update () {

	}

	public void checkHighScore()
	{
		if (playerScore == 0)
			return;

		GameFile.Load ("save.data", ref mysave);

		if (mysave.highScores.Count == 0) {
			 
			mysave.highScores.Add(playerScore);

		} else {
		 
				for (int i = 0; i < mysave.highScores.Count; ++i) {
					if (playerScore >= mysave.highScores [i])
					{
						mysave.highScores.Insert (i, playerScore);
						break;
					}
				}
			 

			if (mysave.highScores.Count > 5)
				mysave.highScores.RemoveRange (5, mysave.highScores.Count - 5);
		}



		GameFile.Save ("save.data", mysave);
	}
}
