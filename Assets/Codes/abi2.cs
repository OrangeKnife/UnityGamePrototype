using UnityEngine;
using System.Collections;

public class abi2 : AbilityBase {
	private PlayerController playerCtrl;

	public override void EnableAbilityPassive()
	{
		print ("test abi2");
		playerCtrl = GetComponent<PlayerController>();
		float gravity = playerCtrl.getPlayerGravity ();
		gravity -= 50;
		playerCtrl.setPlayerGravity (gravity);
		
		base.EnableAbilityPassive();
	}
}
