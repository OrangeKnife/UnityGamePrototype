using UnityEngine;
using System.Collections;

/// <summary>
/// Level section script.
/// Keep data about section here
/// </summary>
public class LevelSectionScript : MonoBehaviour {

	public Bounds boundingBox;
	private GameObject player;
	private int lvScore = 10; // this score value is depended on level
	private PlayerManager _playerManager;// = new PlayerManager();

	private float scoreMultiplier = 1.0f;
	GameManager gameMgr;
	// Use this for initialization
	void Start () {


		gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();

		//player = GameObject.FindGameObjectWithTag ("Player");
		player = gameMgr.GetCurrentPlayer();

		//print ((privateBounding.max.x/3)+", player: "+player.transform.position.x );
		_playerManager = player.GetComponent<PlayerManager>();
	}
	
	public float getScoreMultiplier() {
		return scoreMultiplier;
	}
	
	private int getCurrentRealScore(float score) {
		return Mathf.RoundToInt(score * scoreMultiplier);
	}

	public void setScoreMultiplier(float scoreMulti) {
		scoreMultiplier = scoreMulti;
	}

	public void setLvScore(int score) {
		lvScore = getCurrentRealScore(score);
	}

	private float getPlayerPositionX() {
		return player.transform.position.x;
	}

	// Update is called once per frame
	void Update () {
		//_playerManager.addPlayerScore(lvScore);
		//print ("boundarymax.x: "+(boundingBox.max.x)+", player: "+player.transform.position.x );print ((boundingBox.max.x/3)+", player: "+player.transform.position.x );
		if (getPlayerPositionX() > boundingBox.max.x && lvScore > 0) {
			_playerManager.addPlayerScore(lvScore);
			lvScore = 0;

//			_playerManager.addPlayerScore(lvScore);
//			lvScore = 0;
			//print ("item: score: "+_playerManager.getPlayerScore());
		}
	}
}
