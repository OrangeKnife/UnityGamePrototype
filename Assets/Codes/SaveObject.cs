
using System;
using System.Collections.Generic;

[Serializable]
public class SaveObject
{
	public SaveObject(string inFirstRun)
	{
		firstRun = inFirstRun;

		optionMusic = true;

		highScores = new List<int> ();
	}
	public string firstRun;// = "True";

	public int lastSelectedCharacterIndex;//in selector
	public int lastSelectedAbilityIndex;//in selector

	public bool optionMusic;

	public List<int> highScores;
}

