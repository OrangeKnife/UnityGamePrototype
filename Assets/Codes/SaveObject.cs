
using System;

[Serializable]
public class SaveObject
{
	public SaveObject(string inFirstRun, int inGold)
	{
		firstRun = inFirstRun;
		playerGold = inGold;
	}
	public string firstRun;// = "True";
	public int playerGold;

	public int lastSelectedCharacterIndex;//in selector
	public int lastSelectedAbilityIndex;//in selector

	public bool[] characterUnlockArray;
}

