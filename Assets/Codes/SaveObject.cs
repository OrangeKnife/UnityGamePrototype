
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
}

