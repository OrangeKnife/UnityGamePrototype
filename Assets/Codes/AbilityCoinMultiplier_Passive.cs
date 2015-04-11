using UnityEngine;
using System.Collections;

public class AbilityCoinMultiplier_Passive : AbilityBase {

	private PlayerController playerCtrl;
	private PlayerManager playerMan;
	
	public override void EnableAbilityPassive()
	{
		playerMan = GetComponent<PlayerManager>();
		playerMan.setCoinMultiplier(playerMan.getCoinMultiplier()*2);
		
		base.EnableAbilityPassive();
	}
}
