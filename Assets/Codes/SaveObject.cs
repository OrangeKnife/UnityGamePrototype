using UnityEngine;
using System;

[Serializable]
public class SaveObject
{
	public SaveObject(string inKey, string inValue)
	{
		key = inKey; value = inValue;
	}
	public string key;
	public string value;
}

