using UnityEngine;
using System.Collections;

public class abi2 : MonoBehaviour {
	private PlayerController playerCtrl;
	// Use this for initialization
	void Start () {
		print ("test abi2");
		playerCtrl = GetComponent<PlayerController>();
		float gravity = playerCtrl.getPlayerGravity ();
		gravity -= 50;
		playerCtrl.setPlayerGravity (gravity);
	}

	
	// Update is called once per frame
	void Update () {
	
	}
}
