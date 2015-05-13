using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {
	private GameObject player;
	private PlayerManager _playerManager;
	GameManager gameMgr;
	private Audios sound;

	private bool bCanSpawn = true;
	private float ChanceToSpawn = 20.0f;
	public bool bForceSpawn = false;

	public int CoinValue = 5;

	// Use this for initialization
	void Start () {
		gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();
		player = gameMgr.GetCurrentPlayer();
		_playerManager = player.GetComponent<PlayerManager>();

		InitCoin();
	}

	public void SetSpawnCoin(bool val, bool bSetCanSpawn, bool bSetForceSpawn)
	{
		if (bSetCanSpawn)
			bCanSpawn = val;
		if (bSetForceSpawn)
			bForceSpawn = val;

		InitCoin();
	}

	void InitCoin()
	{
		///// coin is spawned by default
		if (bForceSpawn) 
		{
			// spawn coin
			ShowCoin();
			return;
		}
		else if (bCanSpawn)
		{
			if (Random.Range(0.0f, 100.0f) < ChanceToSpawn)
			{
				// spawn coin
				ShowCoin();
				return;
			}
			else
			{
				// don't spawn coin
				HideCoin();
			}
		}
		else
		{
			// don't spawn coin
			HideCoin();
		}
	}

	void HideCoin()
	{
		GetComponent<Collider2D>().enabled = false;
		GetComponent<Renderer>().enabled = false;
	}

	void ShowCoin()
	{
		GetComponent<Collider2D>().enabled = true;
		GetComponent<Renderer>().enabled = true;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		print ("test coin");
		if (collider.gameObject.tag == "Player") {
			CollectCoin (collider);
		}
		
	}
	void PlayCoinSound() 
	{
		gameMgr.playSound ("coin");
	}
	void CollectCoin(Collider2D coinCollider)
	{
		_playerManager.addCoin(CoinValue);
		PlayCoinSound ();
		Destroy(gameObject);
	}
}
