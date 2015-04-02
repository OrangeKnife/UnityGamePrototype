using UnityEngine;
using System.Collections;

public class AbilityBase : MonoBehaviour {

	public bool bActiveAbility;

	void Start()
	{
		EnableAbilityPassive();
	}

	public virtual bool IsActiveAbility()
	{
		return bActiveAbility;
	}
	public virtual void EnableAbilityActive()
	{
		print("Enable Active Base");
	}
	public virtual void DisableAbilityActive()
	{
		print("Disable Active Base");
	}
	public virtual void EnableAbilityPassive(){}
	public virtual void DisableAbilityPassive(){}
	public virtual float GetTotalCooldown(){return 0;}
	public virtual float GetRemainingCooldown(){return 0;}
	public virtual float GetActiveTotalDuration(){return 0;}
	public virtual float GetActiveRemainingDuration(){return 0;}
}
