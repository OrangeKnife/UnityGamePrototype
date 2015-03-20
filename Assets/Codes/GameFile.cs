using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class GameFile {
	//it's static so we can call it from anywhere
	public static string getSavePath()
	{
		string savepath;
		 
		savepath = Application.persistentDataPath;
		 
		return savepath;
	}
	public static void Save(string saveName, SaveObject save) {
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (getSavePath() + "/" + saveName); //you can call it anything you want
		bf.Serialize(file, save);
		file.Close();
	}   
	
	public static bool Load(string saveName, ref SaveObject save) {
		if(File.Exists(getSavePath() + "/" + saveName)) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/" + saveName, FileMode.Open);
			save = (SaveObject)bf.Deserialize(file);
			file.Close();
			return true;
		}
		return false;
	}
}
 
