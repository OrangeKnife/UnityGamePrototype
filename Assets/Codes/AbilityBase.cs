using UnityEngine;
using System.Collections;

public class AbilityBase : MonoBehaviour {

	public bool bActiveAbility;
	private static float CDTIMER = 7.0f;
	private static float ACTIVETIMER = 4.0f;
	private float active_remain = ACTIVETIMER;
	private float timer = CDTIMER;
	private float startAbilityTime;
	private float startCoolDownTime;

	private bool isActiveEnable;

	void Start()
	{
		print ("start abi");
		EnableAbilityPassive();
	}
	void Update()
	{
		print("duration : "+GetActiveRemainingDuration());
		print("cooldown : "+GetRemainingCooldown());


		if (isActiveEnable && GetActiveRemainingDuration() < 0)
		{
			StopActiveEffect();
		}
	}
	public virtual bool IsActiveAbility()
	{
		return bActiveAbility;
		//return timer > 0 ? true : false;
	}
	public virtual void EnableAbilityActive()
	{
		if (GetRemainingCooldown () < 0) 
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
		return CDTIMER - _currentCDTime;
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
}
