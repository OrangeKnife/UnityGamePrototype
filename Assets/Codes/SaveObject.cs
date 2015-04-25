
using System;

[Serializable]
public class SaveObject
{
	public SaveObject(string inFirstRun)
	{
		firstRun = inFirstRun;
		abilityUnlockedArray = new bool[50];
		abilityUnlockedArray [0] = true;

		optionMusic = true;
	}
	public string firstRun;// = "True";

	public int lastSelectedCharacterIndex;//in selector
	public int lastSelectedAbilityIndex;//in selector

	public bool[] abilityUnlockedArray;

	public bool optionMusic;
}

