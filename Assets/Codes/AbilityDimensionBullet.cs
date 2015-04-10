using UnityEngine;
using System.Collections;

public class AbilityDimensionBullet : AbilityBase {

	private PlayerController playerCtrl;
	private Rigidbody2D tmpRigid;
	private GameObject bullet;
	
	void Start () {
		bActiveAbility = true;

		CDTIMER = 8.0f;
		ACTIVETIMER = 2.0f;

		IconSprite = Resources.Load<Sprite>("Ability/Icon/AbilityIcon_2");
	}
	
	public override void StartActiveEffect()
	{
		if (playerCtrl == null)
			playerCtrl = GetComponent<PlayerController>();
		
		bullet = (GameObject)Instantiate(Resources.Load("Ability/DimensionBullet"),playerCtrl.transform.position + new Vector3(0.5f, 0.0f, 0.0f),Quaternion.identity);
		bullet.GetComponent<DimensionBulletScript>().velocity = new Vector2( playerCtrl.getMoveSpeed()*3.0f, 0 );
		
		base.StartActiveEffect();
	}
	
	public override void StopActiveEffect()
	{
		if (playerCtrl == null)
			playerCtrl = GetComponent<PlayerController>();

		Destroy(bullet);
		base.StopActiveEffect();
	}
}