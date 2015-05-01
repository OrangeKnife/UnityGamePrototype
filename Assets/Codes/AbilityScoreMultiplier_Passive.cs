using UnityEngine;
using System.Collections;

public class AbilityScoreMultiplier_Passive : AbilityBase {

	private LevelSectionScript lvSection;
	GameManager gameMgr;
	private GameObject player;
	private PlayerManager _playerManager;
	
	public override void EnableAbilityPassive()
	{
		gameMgr = GameObject.Find("GameManager").GetComponent<GameManager>();

		player = gameMgr.GetCurrentPlayer();
		_playerManager = player.GetComponent<PlayerManager>();
		_playerManager.setScoreMultiplier (2f);

		base.EnableAbilityPassive();
	}
	
	public override Sprite GetIcon()
	{
		if (IconSprite == null)
			IconSprite = Resources.Load<Sprite>("Ability/Icon/AbilityIcon_2");
		
		return IconSprite;
	}
}
