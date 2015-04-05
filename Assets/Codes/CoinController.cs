using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {
	private GameObject player;
	private PlayerManager _playerManager;
	GameManager gameMgr;
	// Use this for initialization
	void Start () {
		gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();
		player = gameMgr.GetCurrentPlayer();
		_playerManager = player.GetComponent<PlayerManager>();
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		print ("test "+collider.tag);
		if (collider.gameObject.tag == "Player") {
			print ("add coins");
			CollectCoin (collider);
		}
		
	}
	void CollectCoin(Collider2D coinCollider)
	{
		_playerManager.addCoin(1);
		Destroy(gameObject);
	}
}
