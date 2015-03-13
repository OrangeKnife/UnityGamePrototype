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
	private PlayerManager _playerManager = new PlayerManager();
	
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		//print ((privateBounding.max.x/3)+", player: "+player.transform.position.x );
		_playerManager = player.GetComponent<PlayerManager>();
	}

	public void setLvScore(int score) {
		lvScore = score;
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
