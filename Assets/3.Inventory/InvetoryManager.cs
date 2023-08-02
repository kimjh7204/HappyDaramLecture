using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvetoryManager : MonoBehaviour
{
    public GameObject itemUIPrefab;
    public RectTransform inventoryPanel;
    public RectTransform dragLayer;

    public Tooltip tooltip;

    public ItemUI draggingItem = null;

    public ItemSlot selectedSlot = null;

    [SerializeField] private List<ItemSlot> itemSlots = new List<ItemSlot>();

    private void Start()
    {
        for(var i = 0;  i < itemSlots.Count; i++)
        {
            itemSlots[i].Init(this);
        }

        SetItem("Item2");
        SetItem("Item1");
    }

    public void SetItem(string itemName)
    {
        foreach (var itemSlot in itemSlots)
        {
            if (itemSlot.item == null)
            {
                GameObject tempItemUI = Instantiate(itemUIPrefab, itemSlot.transform);
                ItemUI temp = tempItemUI.GetComponent<ItemUI>();
                MyItem tempItemData = Resources.Load<MyItem>("Items/" + itemName);
                temp.Init(tempItemData, this, itemSlot);
                break;
            }
        }
    }
}
