
using System;

[Serializable]
public class SaveObject
{
	public SaveObject(string inFirstRun)
	{
		firstRun = inFirstRun;

		optionMusic = true;
	}
	public string firstRun;// = "True";

	public int lastSelectedCharacterIndex;//in selector
	public int lastSelectedAbilityIndex;//in selector

	public bool optionMusic;
}

