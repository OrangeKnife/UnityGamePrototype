using UnityEngine;
using System.Collections;

public class abi1 : AbilityBase {
	private PlayerController playerCtrl;

	public override void EnableAbilityPassive()
	{
		//print ("test abi1");
		playerCtrl = GetComponent<PlayerController>();
		
		print ("speed0: " + playerCtrl.getMoveSpeed ());
		float speed = playerCtrl.getMoveSpeed ();
		speed -= 6;
		playerCtrl.setMoveSpeed (speed);
		print ("speed: " + speed);

		base.EnableAbilityPassive();
	}
}
