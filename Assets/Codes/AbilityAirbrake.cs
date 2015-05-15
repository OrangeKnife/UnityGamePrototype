using UnityEngine;
using System.Collections;

public class AbilityAirbrake : AbilityBase {

	private PlayerController playerCtrl;
	private float tmpSpeed;

	private float ChargeTimeThreshold = 0.5f;
	private float ChargeTime;

	new void Start () {
		bActiveAbility = true;

		base.Start();
	}
	
	public override void StartActiveEffect()
	{
		if (playerCtrl == null)
			playerCtrl = GetComponent<PlayerController>();

		tmpSpeed = playerCtrl.getMoveSpeed ();
		playerCtrl.setMoveSpeed(0.01f);
		ChargeTime = 0.0f;

		base.StartActiveEffect();
	}

	public override void StopActiveEffect()
	{
		if (playerCtrl == null)
			playerCtrl = GetComponent<PlayerController>();

		playerCtrl.setMoveSpeed(tmpSpeed);
		base.StopActiveEffect();
	}

	new void Update()
	{
		if (playerCtrl != null && playerCtrl.bActivateGlide)
		{
			ChargeTime += Time.deltaTime;
			if (ChargeTime > ChargeTimeThreshold)
			{
				StopActiveEffect();
			}
		}
		else
		{
			ChargeTime = 0.0f;
		}

		base.Update();
	}

	public override Sprite GetIcon()
	{
		if (IconSprite == null)
			IconSprite = Resources.Load<Sprite>("Ability/Icon/AbilityIcon_2");
		
		return IconSprite;
	}
}
