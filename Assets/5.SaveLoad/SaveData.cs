using UnityEngine;

public abstract class GameData
{
	public abstract string GetDirectory();
	public abstract string GetFullPath();
} 

//Outer class
public class MyGameData : GameData
{
	public int lv;
	public string playerName;
	public bool[] options;
	public int numOfObject;
	// public ObjectData[] objectsData;
	//
	//
	// //Inner class
	// public class ObjectData
	// {
	// 	public string key;
	// 	public Vector3 pos;
	// 	public Quaternion rot;
	// }

	public override string GetDirectory()
	=> Application.persistentDataPath + "/MyGameData";

	public override string GetFullPath()
	=> GetDirectory() + "/GameData.json";
}

//-----------------------------------
public class MyGameData1
{

}

public class MyGameData2
{
	
}