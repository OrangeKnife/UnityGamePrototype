using UnityEngine;
using System.Collections;

public class AbilityBase : MonoBehaviour {

	public bool bActiveAbility;
	private static float CDTIMER = 10.0f;
	private static float ACTIVETIMER = 2.0f;
	private float active_remain = ACTIVETIMER;
	private float timer = CDTIMER;
	void Start()
	{
		EnableAbilityPassive();
	}
	void Update()
	{
		timer -= Time.deltaTime;
		active_remain -= Time.deltaTime;
		
		print ("cooldown remain timer: " + timer);
		print ("active_remain timer: " + active_remain);
	}
	public virtual bool IsActiveAbility()
	{
		return bActiveAbility;
		//return timer > 0 ? true : false;
	}
	public virtual void EnableAbilityActive()
	{
		print("Enable Active Base");
		bActiveAbility = true;
	}
	public virtual void DisableAbilityActive()
	{
		print("Disable Active Base");
		bActiveAbility = false;
	}
	public virtual void EnableAbilityPassive(){}
	public virtual void DisableAbilityPassive(){}
	public virtual float GetTotalCooldown(){return CDTIMER;}
	public virtual float GetRemainingCooldown(){return timer;}
	public virtual float GetActiveTotalDuration(){return ACTIVETIMER;}
	public virtual float GetActiveRemainingDuration(){return active_remain;}
}
