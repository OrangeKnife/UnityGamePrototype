using UnityEngine;
using System.Collections;

public class AbilitySlowTime : AbilityBase {

	private PlayerController playerCtrl;
	private float tmpSpeed;
	private float TimeSlowFactor = 0.3f;
	SpriteRenderer tmp;

	new void Start () {
		bActiveAbility = true;

		CDTIMER = 10.0f;
		ACTIVETIMER = 2.5f;

		base.Start();
	}
	
	public override void StartActiveEffect()
	{
		if (playerCtrl == null)
			playerCtrl = GetComponent<PlayerController>();
		
		tmpSpeed = playerCtrl.getJumpForce ();
		playerCtrl.setJumpForce(tmpSpeed * (1/TimeSlowFactor));

		Time.timeScale = TimeSlowFactor;
		Time.fixedDeltaTime = 0.02F * Time.timeScale;

		transform.FindChild("SlowTimeEffect").GetComponent<SpriteRenderer>().enabled = true;

		base.StartActiveEffect();
	}
	
	public override void StopActiveEffect()
	{
		if (playerCtrl == null)
			playerCtrl = GetComponent<PlayerController>();
		
		playerCtrl.setJumpForce(tmpSpeed);

		Time.timeScale = 1.0f;
		Time.fixedDeltaTime = 0.02F * Time.timeScale;

		transform.FindChild("SlowTimeEffect").GetComponent<SpriteRenderer>().enabled = false;
		base.StopActiveEffect();
	}
}
