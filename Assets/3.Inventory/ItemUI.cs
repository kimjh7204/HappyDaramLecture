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
        //invetoryManager.tooltip.Disable();
        itemImage.raycastTarget = false;
        
        rectTransform.SetParent(invetoryManager.dragLayer);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        rectTransform.SetParent(itemSlot.transform);
        rectTransform.localPosition = Vector3.zero;

        //invetoryManager.tooltip.SetTooltip(rectTransform.position, itemData.description);
        itemImage.raycastTarget = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }
}
