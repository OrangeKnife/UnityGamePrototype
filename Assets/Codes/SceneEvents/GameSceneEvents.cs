using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Advertisements;
using GoogleMobileAds.Api;

public class GameSceneEvents : MonoBehaviour {

	[SerializeField]
	GameObject UI_DeathPanel = null;
	[SerializeField]
	GameObject UI_ScorePanel = null;
	[SerializeField]
	GameObject UI_ScoreText = null;
	[SerializeField]
	GameObject UI_AbilityPanel = null;
	[SerializeField]
	GameObject UI_CoinsText = null;

	[SerializeField]
	GameObject UI_ScoreToGold_Score = null;
	
	[SerializeField]
	GameObject UI_ScoreToGold_Gold = null;
	[SerializeField]
	GameObject AbilityUISlotTemplate = null;

	GameObject Player;

	PlayerManager playerMgr = null;
	bool bTickScoreToGold = false;
	int finalScore = 0,finalGold = 0;
	int bTickSpeed_SocrePerTick = 1;

	GameManager gameMgr;


	List<GameObject> abilityUISlots = new List<GameObject>();


	BannerView bannerView;
	AdRequest request;
	void Start () {


		UI_DeathPanel.SetActive (false);

		if (gameMgr == null)
			InitGameMgr ();

	}

	void Awake() {
		if (Advertisement.isSupported) {
			Advertisement.allowPrecache = true;
			Advertisement.Initialize("33340");
		} else {
			Debug.Log("Platform not supported");
		}

		//ca-app-pub-7183026460514946/5522304910 is for IOS now
		//ca-app-pub-7183026460514946/2010435315 is for android
		// Create a 320x50 banner at the top of the screen.

		string bannerAdsId="";
		#if UNITY_IOS && !UNITY_EDITOR
		bannerAdsId = "ca-app-pub-7183026460514946/5522304910";
		#endif
		#if UNITY_ANDROID && !UNITY_EDITOR
		bannerAdsId = "ca-app-pub-7183026460514946/2010435315";
		#endif
		if (bannerView == null) {
			bannerView = new BannerView (
			bannerAdsId, AdSize.Banner, AdPosition.Top);
			// Create an empty ad request.
			request = new AdRequest.Builder ().Build ();
			// Load the banner with the request.
			bannerView.LoadAd (request);
			bannerView.Hide ();
		}
		else
			bannerView.Hide ();
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
				playerMgr.addCoin(finalGold);//will save the gold
			}
		}
	}

	public void onPlayerDead() 
	{
		Invoke ("ShowDeathPanel", 2f);
	}

	public void onPlayerRespawn()
	{
		UpdateUISocre (0);
	}

	void ShowDeathPanel()
	{	
		UI_DeathPanel.SetActive (true);
		UI_ScorePanel.SetActive (false);

		bTickScoreToGold = true;

		finalScore = playerMgr.getPlayerScore ();
		finalGold = 0;

		if(bannerView!=null)
			bannerView.Show ();

	}

	public void OnTryAgainButtonClicked()
	{
		if(bannerView!=null)
			bannerView.Hide ();

		if(finalScore > 0)
		{
			bTickScoreToGold = false;
			playerMgr.addCoin(finalGold + (int)(finalScore * playerMgr.Score2GoldRatio + 0.5f));//will save the gold
		}

		gameMgr.RespawnPlayer();

		UI_DeathPanel.SetActive (false);
		UI_ScorePanel.SetActive (true);


	}

	public void OnChangeCharacterButtonClicked()
	{
		
		if(bannerView!=null)
			bannerView.Hide ();

		if(finalScore > 0)
		{
			bTickScoreToGold = false;
			playerMgr.addCoin(finalGold + (int)(finalScore * playerMgr.Score2GoldRatio + 0.5f));//will save the gold
		}

		gameMgr.EndGame ();
		SceneManager.OpenScene ("CharacterSelection");
	}
	
	public void UpdateUICoins(int newCoins)
	{
		UI_CoinsText.GetComponent<UnityEngine.UI.Text>().text = newCoins.ToString();
	}

	public void UpdateUISocre(int newScore)
	{
		UI_ScoreText.GetComponent<UnityEngine.UI.Text>().text = newScore.ToString();
	}

	public void CleanUpAbilityUISlots()
	{
		abilityUISlots.Clear ();
	}

	public GameObject CreateAbilityUISlot(Vector3 offset)
	{
		GameObject slot = GameObject.Instantiate (AbilityUISlotTemplate);
		slot.transform.SetParent(UI_AbilityPanel.transform,false);
		slot.transform.position += offset;
		abilityUISlots.Add (slot);
		return slot;

	}

	public void ShowAds()
	{
		if(Advertisement.isReady())
		{ Advertisement.Show(null, new ShowOptions{ pause = true,
				resultCallback = ShowResult =>{
					playerMgr.addCoin(100);
				}
			});
		}

	}

}
