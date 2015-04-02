using UnityEngine;
using System.Collections;

public class GameSceneEvents : MonoBehaviour {

	[SerializeField]
	GameObject UI_DeathPanel = null;
	[SerializeField]
	GameObject UI_ScorePanel = null;
	[SerializeField]
	GameObject UI_ScoreText = null;
	[SerializeField]
	GameObject UI_GoldeText = null;
	

	[SerializeField]
	GameObject UI_ScoreToGold_Score = null;
	
	[SerializeField]
	GameObject UI_ScoreToGold_Gold = null;

	public GameObject Player;

	PlayerManager playerMgr = null;
	bool bTickScoreToGold = false;
	int finalScore = 0,finalGold = 0;
	int bTickSpeed_SocrePerTick = 1;

	GameManager gameMgr;

	// Use this for initialization
	void Start () {


		UI_DeathPanel.SetActive (false);

		if (gameMgr == null)
			InitGameMgr ();

	}

	void InitGameMgr()
	{
		gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();
		gameMgr.RespawnPlayer ();
		gameMgr.StartGame ();
		if (playerMgr == null)
			playerMgr = gameMgr.GetCurrentPlayer().GetComponent<PlayerManager> ();

	}

	
	// Update is called once per frame
	void Update () {

//		if (gameMgr == null) {
//			InitGameMgr ();
//			return;
//		}



		if (playerMgr == null)
			playerMgr = gameMgr.GetCurrentPlayer().GetComponent<PlayerManager> ();

		if (bTickScoreToGold) {
			finalScore -= bTickSpeed_SocrePerTick;
			finalGold += (int)(bTickSpeed_SocrePerTick * playerMgr.Score2GoldRatio + 0.5f);
			UI_ScoreToGold_Score.GetComponent<UnityEngine.UI.Text>().text = finalScore.ToString();
			UI_ScoreToGold_Gold.GetComponent<UnityEngine.UI.Text>().text = finalGold.ToString();

			if(finalScore <= 0)
			{
				bTickScoreToGold = false;
				playerMgr.addGold(finalGold);//will save the gold
			}
		}
	}

	public void onPlayerDead() 
	{
		Invoke ("ShowDeathPanel", 2f);
	}

	void ShowDeathPanel()
	{	
		UI_DeathPanel.SetActive (true);
		UI_ScorePanel.SetActive (false);

		bTickScoreToGold = true;

		finalScore = playerMgr.getPlayerScore ();
		finalGold = 0;
	}

	public void OnTryAgainButtonClicked()
	{
		Debug.Log("TryAgain!");
		//Player.GetComponent<PlayerController> ().Respawn();
		gameMgr.RespawnPlayer();

		UI_DeathPanel.SetActive (false);
		UI_ScorePanel.SetActive (true);
	}

	public void OnChangeCharacterButtonClicked()
	{
		gameMgr.EndGame ();
		SceneManager.OpenScene ("CharacterSelection");
	}

	public void UpdateUISocre(int newScore)
	{
		UI_ScoreText.GetComponent<UnityEngine.UI.Text>().text = newScore.ToString();
	}

	public void UpdateUIGold(int newGold)
	{
		UI_GoldeText.GetComponent<UnityEngine.UI.Text>().text = newGold.ToString();
	}
}
