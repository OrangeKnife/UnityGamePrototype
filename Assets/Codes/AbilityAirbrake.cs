using UnityEngine;
using System.Collections;

public class AbilityAirbrake : AbilityBase {

	private PlayerController playerCtrl;
	private float tmpSpeed;

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

		base.StartActiveEffect();
	}

	public override void StopActiveEffect()
	{
		if (playerCtrl == null)
			playerCtrl = GetComponent<PlayerController>();

		playerCtrl.setMoveSpeed(tmpSpeed);
		base.StopActiveEffect();
	}
}
