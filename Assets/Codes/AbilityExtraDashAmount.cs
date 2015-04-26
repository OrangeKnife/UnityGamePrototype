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

	public override Sprite GetIcon()
	{
		if (IconSprite == null)
			IconSprite = Resources.Load<Sprite>("Ability/Icon/AbilityIcon_2");
		
		return IconSprite;
	}
}
