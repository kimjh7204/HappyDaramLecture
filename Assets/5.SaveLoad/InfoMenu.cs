using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestMenu : MonoBehaviour
{
    public TextMeshProUGUI playerName;

    public TextMeshProUGUI levelText;
    private int level = 1;

    public Toggle[] systemOptions;
    
    public TextMeshProUGUI numOfObjText;
    private int numOfObj = 1;

    private readonly Dictionary<string, GameObject> loadedObjects = new Dictionary<string, GameObject>();
    private string[] objectKeys;

    private List<GameObject> generatedObject = new List<GameObject>();
    
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
        levelText.text = level.ToString();
    }
    
    public void SetNumOfObject(int inc)
    {
        numOfObj += inc;
        numOfObjText.text = numOfObj.ToString();
    }

    public void GenerateObjects()
    {
        foreach (var obj in generatedObject)
        {
            Destroy(obj);
        }
        
        generatedObject.Clear();

        for (var i = 0; i < numOfObj; i++)
        {
            var randomIdx = Random.Range(0, objectKeys.Length); //[0, objectKeys.Length)
            var randomPos = new Vector3(Random.Range(-FieldWidth, FieldWidth), 1f, Random.Range(-FieldHeight, FieldHeight));
            
            var obj = Instantiate(loadedObjects[objectKeys[randomIdx]], randomPos, Random.rotation);
            
            generatedObject.Add(obj);
        }
    }

    public void OpenMenu(bool open)
    {
        gameObject.SetActive(open);
    }
}
