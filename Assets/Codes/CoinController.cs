using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {
	private GameObject player;
	private PlayerManager _playerManager;
	GameManager gameMgr;
	private Audios sound;
	public int CoinValue = 5;

	// Use this for initialization
	void Start () {
		gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();
		player = gameMgr.GetCurrentPlayer();
		_playerManager = player.GetComponent<PlayerManager>();
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
