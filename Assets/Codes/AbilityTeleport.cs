using UnityEngine;
using System.Collections;

public class AbilityTeleport : AbilityBase {

	private PlayerController playerCtrl;
	private float TeleportOffsetX = 7.0f;
	private float TeleportFreezeTime = 0.3f;
	Vector3 teleportToLocation;
	bool startInterpolation;
	
	void Start () {
		bActiveAbility = true;
		
		CDTIMER = 5.0f;
		ACTIVETIMER = 2.0f;
		
		IconSprite = Resources.Load<Sprite>("Ability/Icon/AbilityIcon_2");
	}
	
	public override void StartActiveEffect()
	{
		if (playerCtrl == null)
			playerCtrl = GetComponent<PlayerController>();
		
		if (playerCtrl)
		{
			freezePlayer();
			Invoke ("doTeleporting", TeleportFreezeTime);
		}

		base.StartActiveEffect();
	}
	
	public override void StopActiveEffect()
	{
		if (playerCtrl == null)
			playerCtrl = GetComponent<PlayerController>();
		

		base.StopActiveEffect();
	}

	void doTeleporting()
	{
		teleportToLocation = playerCtrl.gameObject.transform.position + new Vector3(TeleportOffsetX, 0.0f, 0.0f);
		startInterpolation = true;
//		playerCtrl.gameObject.transform.position = playerCtrl.gameObject.transform.position + new Vector3(TeleportOffsetX, 0.0f, 0.0f);
//		Invoke ("unFreezePlayer", 0.5f);
	}

	void freezePlayer()
	{
		//playerCtrl.GetComponent<Collider2D>().enabled = false;
		playerCtrl.freeze ();

		playerCtrl.SetCheckDeadRaycast(false);
	}
	
	void unFreezePlayer()
	{
		//playerCtrl.GetComponent<Collider2D>().enabled = true;
		playerCtrl.unFreeze ();

		playerCtrl.SetCheckDeadRaycast(true);
	}

	new void Update () {
		
		if (startInterpolation && playerCtrl) 
		{
			GameObject playerObj = playerCtrl.gameObject;
			playerObj.transform.position = Vector3.Lerp(playerObj.transform.position,teleportToLocation,0.2f);
			
			//if((playerObj.transform.position - teleportToLocation).sqrMagnitude < 0.01f || playerObj.transform.position.x > teleportToLocation.x)
			if((playerObj.transform.position - teleportToLocation).sqrMagnitude < 0.01f)
			{
				startInterpolation = false;
				Invoke ("unFreezePlayer", TeleportFreezeTime);
			}
		}

		base.Update();
	}
}
