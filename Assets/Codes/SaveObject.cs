
using System;

[Serializable]
public class SaveObject
{
	public SaveObject(string inFirstRun, int inGold)
	{
		firstRun = inFirstRun;
		playerGold = inGold;
		characterUnlockedArray = new bool[50];
		characterUnlockedArray [0] = true;
		abilityUnlockedArray = new bool[50];
		abilityUnlockedArray [0] = true;

		optionMusic = true;
	}
	public string firstRun;// = "True";
	public int playerGold;

	public int lastSelectedCharacterIndex;//in selector
	public int lastSelectedAbilityIndex;//in selector

	public bool[] characterUnlockedArray;
	public bool[] abilityUnlockedArray;

	public bool optionMusic;
}

