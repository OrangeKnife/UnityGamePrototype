using UnityEngine;
using System.Collections;

public class AbilityManager : MonoBehaviour {
	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");

		//add ability to player, disabled by comment
		//abi1 : move speed minus 8, disable by commen
		player.AddComponent(typeof(abi1));
		//abi2 : graivty - 50
		player.AddComponent(typeof(abi2));

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
