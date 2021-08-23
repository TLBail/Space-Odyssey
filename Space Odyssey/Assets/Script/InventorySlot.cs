using System;
using System.Collections;
using System.Collections.Generic;
using Script.Item_and_inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class InventorySlot : MonoBehaviour
{
    private Item _item;
    public Image icon;
    public Button removeButton;
    private SlotItemToMove slotItemToMoveObj;

    private Inventaire inventaire;
    
    private void Start()
    {
        slotItemToMoveObj = PlayerManager.Instance.SlotItemToMove;
        inventaire = GameManager.Instance.inventaire;
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
        inventaire.removeItem(_item);
    }


    public void Onclick()
    {
        if (_item == null) return;

        PlayerManager.Instance.selectedItem = _item;
        slotItemToMoveObj.OnMove();
    }
}
