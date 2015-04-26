using UnityEngine;
using System.Collections;

public class AbilityExtraDashTime : AbilityBase {

	private PlayerController playerCtrl;
	
	public override void EnableAbilityPassive()
	{
		playerCtrl = GetComponent<PlayerController>();
		playerCtrl.setMaxGlideTime(playerCtrl.getMaxGlideTime()*1.5f);
		
		base.EnableAbilityPassive();
	}

	public override Sprite GetIcon()
	{
		if (IconSprite == null)
			IconSprite = Resources.Load<Sprite>("Ability/Icon/AbilityIcon_2");
		
		return IconSprite;
	}
}
