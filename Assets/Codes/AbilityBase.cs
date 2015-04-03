using UnityEngine;
using System.Collections;

public class AbilityBase : MonoBehaviour {

	public bool bActiveAbility;
	private static float CDTIMER = 10.0f;
	private static float ACTIVETIMER = 2.0f;
	private float active_remain = ACTIVETIMER;
	private float timer = CDTIMER;
	private System.DateTime startAbilityTime;
	private System.DateTime startCoolDownTime;

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
			bActiveAbility = true;
			startAbilityTime = System.DateTime.Now;
			startCoolDownTime = System.DateTime.Now;
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
		System.TimeSpan _currentCDTime = System.DateTime.Now - startCoolDownTime;
		print ("test tim  e" +startCoolDownTime);
		return System.Convert.ToSingle(_currentCDTime.TotalSeconds);
	}
	public virtual float GetActiveTotalDuration() {
		return ACTIVETIMER;
	}
	public virtual float GetActiveRemainingDuration() {
		System.TimeSpan _currentActiveTime = System.DateTime.Now - startAbilityTime;
		return System.Convert.ToSingle(_currentActiveTime.TotalSeconds);
	}
}
