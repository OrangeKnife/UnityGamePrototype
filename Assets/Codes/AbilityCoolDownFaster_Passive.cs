using UnityEngine;
using System.Collections;

public class AbilityCoolDownFaster_Passive : AbilityBase {

	// Use this for initialization
	private PlayerController playerCtrl;
	private AbilityBase abilityBase;
	
	public override void EnableAbilityPassive()
	{
		abilityBase = GetComponent<AbilityBase>();
		abilityBase.setCDMultipler(abilityBase.getCDMultipler()*0.3f);
		
		base.EnableAbilityPassive();
	}
	
	public override Sprite GetIcon()
	{
		if (IconSprite == null)
			IconSprite = Resources.Load<Sprite>("Ability/Icon/AbilityIcon_2");
		
		return IconSprite;
	}
}
