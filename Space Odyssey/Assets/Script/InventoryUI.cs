using System;
using System.Collections;
using System.Collections.Generic;
using Script.Item_and_inventory;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform itemsParent;
    public InventorySlot[] slots;

    private Inventaire inventaire;
    
    void Start()
    {
        inventaire = GameManager.Instance.gameObject.GetComponent<Inventaire>();
        inventaire.OnInventoryChangeCallBack += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        UpdateUI();
    }

    public void UpdateUI()
    {

        List<Item> itemOfPlayer = inventaire.getItemList();
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < itemOfPlayer.Count)
            {
                slots[i].AddItem(itemOfPlayer[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
