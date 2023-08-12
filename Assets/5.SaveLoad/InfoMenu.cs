using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestMenu : MonoBehaviour
{
    public TMP_InputField playerName;

    public TextMeshProUGUI levelText;
    private int _level = 1;
    private int level
    {
        get => _level;
        set
        {
            _level = value;
            levelText.text = value.ToString();
        }
    }

    public Toggle[] systemOptions;
    
    public TextMeshProUGUI numOfObjText;
    private int _numOfObj = 1;
    private int numOfObj
    {
        get => _numOfObj;
        set
        {
            _numOfObj = value;
            numOfObjText.text = value.ToString();
        }
    }

    private readonly Dictionary<string, GameObject> loadedObjects = new Dictionary<string, GameObject>();
    private string[] objectKeys;

    private List<ObjectData> generatedObjectData = new List<ObjectData>();
    
    private const float FieldWidth = 30f;
    private const float FieldHeight = 20f;
    
    private void Start()
    {
        var objs = Resources.LoadAll<GameObject>("SaveLoadObjects");
        
        foreach (var obj in objs)
        {
            Debug.Log("Loaded : " + obj.name);
            loadedObjects.Add(obj.name, obj);
        }

        objectKeys = loadedObjects.Keys.ToArray();
    }
    
    public void SetLevel(int inc)
    {
        level += inc;
        //levelText.text = level.ToString();
    }
    
    public void SetNumOfObject(int inc)
    {
        numOfObj += inc;
        //numOfObjText.text = _numOfObj.ToString();
    }

    public void GenerateObjects()
    {
        GenerateObjects(null);
    }

    public void GenerateObjects(MyGameData.ObjectData[] data)
    {
        foreach (var objData in generatedObjectData)
        {
            Destroy(objData.gameObject);
        }
        
        generatedObjectData.Clear();

        for (var i = 0; i < _numOfObj; i++)
        {
            string objKey;
            Vector3 pos;
            Quaternion rot;

            if (data is null)
            {
                objKey = objectKeys[Random.Range(0, objectKeys.Length)]; //[0, objectKeys.Length)
                pos = new Vector3(Random.Range(-FieldWidth, FieldWidth), 1f, Random.Range(-FieldHeight, FieldHeight));
                rot = Random.rotation;
            }
            else
            {
                objKey = data[i].key;
                pos = data[i].pos;
                rot = data[i].rot;
            }

            GameObject obj = Instantiate(loadedObjects[objKey], pos, rot);

            ObjectData objectData = obj.AddComponent<ObjectData>();
            objectData.objKey = objKey;

            generatedObjectData.Add(objectData);
        }
    }

    public void OpenMenu(bool open)
    {
        gameObject.SetActive(open);
    }

    public void Save()
    {
        bool[] options = new bool[systemOptions.Length];
        for (var i = 0; i < systemOptions.Length; i++)
        {
            options[i] = systemOptions[i].isOn;
        }


        MyGameData.ObjectData[] objectsData = new MyGameData.ObjectData[generatedObjectData.Count];
        for(var i = 0; i < generatedObjectData.Count; i++)
        {
            objectsData[i] = new MyGameData.ObjectData()
            {
                key = generatedObjectData[i].objKey,
                pos = generatedObjectData[i].transform.position,
                rot = generatedObjectData[i].transform.rotation
            };
        }


        MyGameData data = new()
        {
            lv = level,
            playerName = playerName.text,
            numOfObject = numOfObj,
            options = options,
            objectsData = objectsData,
        };

        SaveLoadManager.SaveData(data);
    }

    public void Load()
    {
        MyGameData data = SaveLoadManager.LoadData<MyGameData>();

        level = data.lv;
        playerName.text = data.playerName;
        numOfObj = data.numOfObject;

        for (var i = 0; i < data.options.Length; i++)
        {
            systemOptions[i].isOn = data.options[i];
        }

        GenerateObjects(data.objectsData);
    }
}
