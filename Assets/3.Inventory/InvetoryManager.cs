using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryManager : MonoBehaviour
{
    public GameObject itemUIPrefab;
    public RectTransform inventoryPanel;
    public RectTransform dragLayer;

    public Tooltip tooltip;

    [SerializeField] private List<ItemSlot> itemSlots = new List<ItemSlot>();
    
    public MyItem testItem;
    
    private void Start()
    {
        for(var i = 0;  i < itemSlots.Count; i++)
        {
            if(itemSlots[i].item == null)
            {
                GameObject tempItemUI = Instantiate(itemUIPrefab, itemSlots[i].transform);
                ItemUI temp = tempItemUI.GetComponent<ItemUI>();
                itemSlots[i].item = temp;

                temp.Init(testItem, this, itemSlots[i]);

                break;
            }
        }

        //var tempItemUI = Instantiate(itemUIPrefab, inventoryPanel);
        //var temp = tempItemUI.GetComponent<ItemUI>();
        
        //temp.Init(testItem, this);
    }
}
