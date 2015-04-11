using UnityEngine;
using System.Collections;

public class AbilityExtraDashAmount : AbilityBase {

	private PlayerController playerCtrl;
	
	public override void EnableAbilityPassive()
	{
		playerCtrl = GetComponent<PlayerController>();
		playerCtrl.setMaxGlideAllow(playerCtrl.getMaxGlideAllow()+1);
		
		base.EnableAbilityPassive();
	}
}
