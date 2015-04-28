using UnityEngine;
using System.Collections;

public class AbilityScoreMultiplier_Passive : AbilityBase {

	private LevelSectionScript lvSection;
	private PlayerManager playerMan;
	
	public override void EnableAbilityPassive()
	{
		lvSection = GetComponent<LevelSectionScript>();
		lvSection.setScoreMultiplier(lvSection.getScoreMultiplier()*2f);
		print ("score : " + lvSection.getScoreMultiplier () * 2f);
		base.EnableAbilityPassive();
	}
	
	public override Sprite GetIcon()
	{
		if (IconSprite == null)
			IconSprite = Resources.Load<Sprite>("Ability/Icon/AbilityIcon_2");
		
		return IconSprite;
	}
}
