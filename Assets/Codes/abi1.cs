using UnityEngine;
using System.Collections;

public class abi1 : MonoBehaviour {
	private PlayerController playerCtrl;
	// Use this for initialization
	void Start () {
		//print ("test abi1");
		playerCtrl = GetComponent<PlayerController>();
		
		print ("speed0: " + playerCtrl.getMoveSpeed ());
		float speed = playerCtrl.getMoveSpeed ();
		speed -= 6;
		playerCtrl.setMoveSpeed (speed);
		print ("speed: " + speed);
	}

	// Update is called once per frame
//	void Update () {
//		print("player ctrl speed: " + playerCtrl.getMoveSpeed ());
//	}
}
