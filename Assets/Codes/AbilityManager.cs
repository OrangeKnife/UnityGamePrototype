using UnityEngine;
using System.Collections;

public class AbilityManager : MonoBehaviour {
	private GameObject player;
	// Use this for initialization
	void Start () {
		//player = GameObject.FindGameObjectWithTag ("Player");
		//print(System.Activator.CreateInstance(System.Type.GetType("abi1")).ToString());
		//add ability to player, disabled by comment

		//abi1 : move speed minus 8, disable by commen
		//player.AddComponent(typeof(abi1));
		//print ("test");
//		addAbility ("abi1");
		//abi2 : graivty - 50
//		addAbility ("abi2");
		//player.AddComponent(typeof(abi2));


	}

	public bool addAbility(string abilityObjectName) {
		player = GameObject.FindGameObjectWithTag ("Player");
		if (player) {
			System.Type t2 = System.Type.GetType (abilityObjectName);

			if (player.AddComponent (t2)) {
				//no error
				return true;
			}
		}
		//cannot add ablity
		return false;
	}

}
