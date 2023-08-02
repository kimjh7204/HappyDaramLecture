using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private MyItem itemData;
    private Image itemImage;
    private RectTransform rectTransform;

    private InvetoryManager invetoryManager;
    private ItemSlot itemSlot;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void Init(MyItem data, InvetoryManager manager, ItemSlot slot)
    {
        itemData = data;
        itemImage = GetComponent<Image>();
        itemImage.sprite = itemData.itemImage;

        invetoryManager = manager;
        itemSlot = slot;
        itemSlot.item = this;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        invetoryManager.tooltip.SetTooltip(rectTransform.position, itemData.description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        invetoryManager.tooltip.Disable();
    }

    //-------------------------------------------------------------------


    public void OnPointerDown(PointerEventData eventData)
    {
        invetoryManager.draggingItem = this;
        itemImage.raycastTarget = false;
        
        rectTransform.SetParent(invetoryManager.dragLayer);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        invetoryManager.draggingItem = null;
        itemImage.raycastTarget = true;
        
        var newItemSlot = invetoryManager.selectedSlot;
        
        if (newItemSlot == null)        //지정된 슬롯 없음 => 자기 자리로 돌아감
        {
            SetItemOnSlot(itemSlot);
            return;
        }
        
        if (newItemSlot.IsItemIn)       //옮기려는 슬롯에 이미 아이템 있음 => Swap
        {
            newItemSlot.item.SetItemOnSlot(itemSlot);
            ChangeItemSlot(newItemSlot);
        }
        else                            //옮기려는 슬롯이 비어있음 => 그냥 Set Item
        {
            SetItemOnSlot(newItemSlot);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    private void ChangeItemSlot(ItemSlot slot)
    {
        itemSlot.item = null;
        SetItemOnSlot(slot);
    }

    private void SetItemOnSlot(ItemSlot slot)
    {
        itemSlot = slot;
        itemSlot.item = this;
        rectTransform.SetParent(itemSlot.transform);
        rectTransform.localPosition = Vector3.zero;
    }
}
