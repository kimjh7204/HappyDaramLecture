using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
	//File -> XML, Binary, Json
	
	//C++ -> Function,     C#, Java -> Method
	//C++ -> Templete,     C#, Java -> Generic
	//객체지향의 다형성 
	
	public static void SaveData<T>(T data) where T : GameData
	{
		//File Path(경로)
		if (!Directory.Exists(data.GetDirectory()))
		{
			Directory.CreateDirectory(data.GetDirectory());
		}

		string json = JsonUtility.ToJson(data);
		File.WriteAllText(data.GetFullPath(), json);

		Debug.Log("-----------------------------------------------");
		Debug.Log("Data saved, path : " + data.GetFullPath());
		Debug.Log("-----------------------------------------------");
	}

	public static T LoadData<T>() where T : GameData, new()
	{
		var data = new T();
		
		if (!File.Exists(data.GetFullPath()))
		{
			Debug.LogError("There is no file : " + data.GetFullPath());
			return null;
		}

		var json = File.ReadAllText(data.GetFullPath());
		data = JsonUtility.FromJson<T>(json);

		Debug.Log("-----------------------------------------------");
		Debug.Log("Data loaded, path : " + data.GetFullPath());
		Debug.Log("-----------------------------------------------");
		
		return data;
	}
}
