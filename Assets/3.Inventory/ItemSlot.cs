using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private InvetoryManager manager;
    public ItemUI item;

    public bool IsItemIn => item != null;
    
    public void Init(InvetoryManager inventoryManager)
    {
        manager = inventoryManager;
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        manager.selectedSlot = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        manager.selectedSlot = null;
    }
}
