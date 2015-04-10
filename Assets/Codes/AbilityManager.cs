using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AbilityManager : MonoBehaviour {

	public List<AbilityBase> AbilityComponents;
	private GameObject player;
	GameManager gameMgr;
	
	void Start () {
		gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public bool addAbility(string abilityObjectName, GameObject inUIIconObject) {
		if (gameMgr == null)
			gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();

		player = gameMgr.GetCurrentPlayer();
		if (player) {
			System.Type t2 = System.Type.GetType (abilityObjectName);
			AbilityBase tmpAbility = (AbilityBase)player.AddComponent (t2);
			if (tmpAbility) 
			{
				AbilityComponents.Add(tmpAbility);
				tmpAbility.bindUIIconObject(inUIIconObject, tmpAbility.GetIcon());
				return true;
			}
		}
		//cannot add ablity
		return false;
	}

	public void ActivateAbility()
	{
		for (int i = 0; i < AbilityComponents.Count; ++i)
		{
			if (AbilityComponents[i].IsActiveAbility())
				AbilityComponents[i].EnableAbilityActive();
		}
	}

	public void DeactivateAbility()
	{
		for (int i = 0; i < AbilityComponents.Count; ++i)
		{
			if (AbilityComponents[i].IsActiveAbility())
				AbilityComponents[i].DisableAbilityActive();
		}
	}

	public void ForceStopActiveAbility()
	{
		for (int i = 0; i < AbilityComponents.Count; ++i)
		{
			if (AbilityComponents[i].IsActiveAbility())
			{
				AbilityComponents[i].DisableAbilityActive();
				AbilityComponents[i].StopActiveEffect();
			}
		}
	}
}
