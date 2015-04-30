using UnityEngine;
using System.Collections;

public class AbilityLowGravity : AbilityBase {

	private PlayerController playerCtrl;
	private bool bActiveInEffect;
	private bool bInTimerRange;
	
	new void Start () {
		bActiveAbility = true;

		CDTIMER = 5.0f;
		ACTIVETIMER = 1.0f;
		
		base.Start();
	}
	
	public override void StartActiveEffect()
	{
		if (playerCtrl == null)
			playerCtrl = GetComponent<PlayerController>();

		bActiveInEffect = true;
		bInTimerRange = true;
		playerCtrl.setPlayerGravity( playerCtrl.getPlayerGravity() * 0.5f );
		
		base.StartActiveEffect();
	}

	new void Update () {

		if (playerCtrl == null)
			playerCtrl = GetComponent<PlayerController>();

		if (playerCtrl.IsGrounded() && bActiveInEffect && !bInTimerRange)
		{
			bActiveInEffect = false;
			playerCtrl.setPlayerGravity( playerCtrl.getPlayerGravity() * 2.0f );
		}

		base.Update();
	}
	
	public override void StopActiveEffect()
	{
		if (playerCtrl == null)
			playerCtrl = GetComponent<PlayerController>();
		
		bInTimerRange = false;
		base.StopActiveEffect();
	}

	public override Sprite GetIcon()
	{
		if (IconSprite == null)
			IconSprite = Resources.Load<Sprite>("Ability/Icon/AbilityIcon_2");
		
		return IconSprite;
	}
}
