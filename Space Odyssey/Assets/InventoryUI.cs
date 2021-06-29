using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    private PlayerManager _playerManager;
    public Transform itemsParent;
    public InventorySlot[] slots;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerManager = PlayerManager.Instance;
        _playerManager.OnInventoryChangeCallBack += UpdateUI;

        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < _playerManager.Inventaire.Count)
            {
                slots[i].AddItem(_playerManager.Inventaire[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
