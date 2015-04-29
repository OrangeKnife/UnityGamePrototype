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
		//InvertAllMaterialColors();
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
		//InvertAllMaterialColors();
		base.StopActiveEffect();
	}

	public override Sprite GetIcon()
	{
		if (IconSprite == null)
			IconSprite = Resources.Load<Sprite>("Ability/Icon/AbilityIcon_2");
		
		return IconSprite;
	}

	public Color InvertColor (Color color)
	{
		return new Color (1.0f-color.r, 1.0f-color.g, 1.0f-color.b);
	}

	public void InvertAllMaterialColors () {
		Renderer[] renderers = FindObjectsOfType<Renderer>();
		foreach (Renderer render in renderers) {
			if (render.material.HasProperty("_Color")) {
				//render.material.color = InvertColor (render.material.color);
				render.material.shader = Resources.Load<Shader>("Ability/InverseColorShader");
			}
		}
	}
}
