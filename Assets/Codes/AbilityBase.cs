using UnityEngine;
using System.Collections;

public class AbilityBase : MonoBehaviour {

	public bool bActiveAbility;
	protected float CDTIMER = 10.0f;
	protected float ACTIVETIMER = 4.0f;
	protected float active_remain;
	protected float timer;
	protected float startAbilityTime;
	protected float startCoolDownTime;

	protected bool isActiveEnable;
	protected Sprite IconSprite;

	GameObject UIIconObjectMask = null;

	void Start()
	{
		print ("start abi");
		active_remain = ACTIVETIMER;
		timer = CDTIMER;
		IconSprite = (Sprite)Resources.Load("Ability/Icon/Combat_64");

		EnableAbilityPassive();
	}
	void Update()
	{
		//print("duration : "+GetActiveRemainingDuration());
		//print("cooldown : "+GetRemainingCooldown());

		float cooldown = GetRemainingCooldown();
		if (cooldown >= 0 && UIIconObjectMask != null) {
			float percentage = Mathf.Max(cooldown / GetTotalCooldown(),0f);
			UIIconObjectMask.GetComponent<UnityEngine.UI.Image>().fillAmount = percentage;
			print (percentage);
			UIIconObjectMask.GetComponent<UnityEngine.UI.Image>().enabled = percentage > 0f;
		}

		if (isActiveEnable && GetActiveRemainingDuration() < 0)
		{
			StopActiveEffect();
		}
	}

	public void bindUIIconObject(GameObject inUIIcon)
	{
		foreach(Transform tf in inUIIcon.GetComponentsInChildren<Transform>())
		{
			if(tf.name == "AbilityMaskImg")
			{
				UIIconObjectMask = tf.gameObject;
				break;
			}
		}
	}

	public virtual bool IsActiveAbility()
	{
		return bActiveAbility;
		//return timer > 0 ? true : false;
	}
	public virtual void EnableAbilityActive()
	{
		if (GetRemainingCooldown () <= 0) 
		{
			print("Enable Active Base");
			startAbilityTime = Time.time;
			startCoolDownTime = Time.time;
			isActiveEnable = true;
			StartActiveEffect();
		}
	}
	public virtual void StartActiveEffect()
	{

	}
	public virtual void DisableAbilityActive()
	{

	}
	public virtual void StopActiveEffect()
	{
		print("Disable Active Base");
		isActiveEnable = false;
	}
	public virtual void EnableAbilityPassive() {

	}
	public virtual void DisableAbilityPassive() {
	}
	public virtual float GetTotalCooldown() {
		return CDTIMER;
	}
	public virtual float GetRemainingCooldown() {
		float _currentCDTime = Time.time - startCoolDownTime;
		//print ("test tim  e" +startCoolDownTime);
		return Mathf.Max(CDTIMER - _currentCDTime,0f) ;
		//return System.Convert.ToSingle(_currentCDTime.TotalSeconds);
	}
	public virtual float GetActiveTotalDuration() {
		return ACTIVETIMER;
	}
	public virtual float GetActiveRemainingDuration() {
		float _currentActiveTime = Time.time - startAbilityTime;
		return ACTIVETIMER - _currentActiveTime;
		//return System.Convert.ToSingle(_currentActiveTime.TotalSeconds);
	}

	public virtual Sprite GetIcon()
	{
		return IconSprite;
	}
}
