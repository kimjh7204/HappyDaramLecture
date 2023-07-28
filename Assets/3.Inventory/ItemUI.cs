using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    private MyItem itemData;
    private Image itemImage;

    public void Init(MyItem data)
    {
        itemData = data;
        itemImage = GetComponent<Image>();
        itemImage.sprite = itemData.itemImage;
    }
}
