using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class GameFile {
	//it's static so we can call it from anywhere
	public static void Save(string saveName, List<SaveObject> saves) {
		BinaryFormatter bf = new BinaryFormatter();
		//Application.persistentDataPath is a string, so if you wanted you can put that into debug.log if you want to know where save games are located
		FileStream file = File.Create (Application.persistentDataPath + "/" + saveName); //you can call it anything you want
		bf.Serialize(file, saves);
		file.Close();
	}   
	
	public static void Load(string saveName, ref List<SaveObject> saves) {
		if(File.Exists(Application.persistentDataPath + "/" + saveName)) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/" + saveName, FileMode.Open);
			saves = (List<SaveObject>)bf.Deserialize(file);
			file.Close();
		}
	}
}
 
