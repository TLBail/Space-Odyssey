using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    private Item _item;
    public Image icon;
    public Button removeButton;
    private SlotItemToMove slotItemToMoveObj;

    private void Start()
    {
        slotItemToMoveObj = PlayerManager.Instance.SlotItemToMove;
    }

    public void AddItem(Item newItem)
    {
        _item = newItem;
        icon.sprite = _item.icon;
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        _item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        PlayerManager.Instance.RemoveItem(_item);
    }


    public void Onclick()
    {
        PlayerManager.Instance.selectedItem = _item;
        slotItemToMoveObj.OnMove();
    }
}
