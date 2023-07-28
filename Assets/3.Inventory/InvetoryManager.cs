using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryManager : MonoBehaviour
{
    public GameObject itemUIPrefab;
    public RectTransform inventoryPanel;

    private List<ItemUI> items = new List<ItemUI>();
    
    public MyItem testItem;
    
    private void Start()
    {
        var tempItemUI = Instantiate(itemUIPrefab, inventoryPanel);
        var temp = tempItemUI.GetComponent<ItemUI>();
        
        items.Add(temp);
        temp.Init(testItem);
    }
}
