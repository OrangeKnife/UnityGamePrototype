using UnityEngine;
using System.Collections;

public class abi1 : MonoBehaviour {
	private PlayerController playerCtrl;
	// Use this for initialization
	void Start () {
		print ("test abi1");
		playerCtrl = GetComponent<PlayerController>();
		float speed = playerCtrl.getMoveSpeed ();
		speed -= 8;
		playerCtrl.setMoveSpeed (speed);

	}

	// Update is called once per frame
	void Update () {
		print("player ctrl speed: " + playerCtrl.getMoveSpeed ());
	}
}
