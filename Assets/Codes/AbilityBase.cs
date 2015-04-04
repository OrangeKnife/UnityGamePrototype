using UnityEngine;
using System.Collections;

public class AbilityBase : MonoBehaviour {

	public bool bActiveAbility;
	private static float CDTIMER = 10.0f;
	private static float ACTIVETIMER = 2.0f;
	private float active_remain = ACTIVETIMER;
	private float timer = CDTIMER;
	private float startAbilityTime;
	private float startCoolDownTime;

	void Start()
	{
		print ("start abi");
		EnableAbilityPassive();
	}
	void Update()
	{
		print("ramain cooldown : "+GetRemainingCooldown());
	}
	public virtual bool IsActiveAbility()
	{
		return bActiveAbility;
		//return timer > 0 ? true : false;
	}
	public virtual void EnableAbilityActive()
	{
		if (GetRemainingCooldown () <= 0) {
			print("Enable Active Base");
			startAbilityTime = Time.time;
			startCoolDownTime = Time.time;
		}


	}
	public virtual void DisableAbilityActive()
	{
		print("Disable Active Base");
		bActiveAbility = false;

		//print ("time span = " + ts);
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
