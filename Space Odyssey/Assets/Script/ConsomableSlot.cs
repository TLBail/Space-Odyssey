using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsomableSlot : MonoBehaviour
{
    
    private Item _item;
    private PlayerManager _playerManager;
    private SlotItemToMove slotItemToMoveObj;
    private Item selectedItem;

    private void Start()
    {
        _playerManager = PlayerManager.Instance;
        slotItemToMoveObj = PlayerManager.Instance.SlotItemToMove;
    }
    

    public void onItemSlotClick()
    {
        selectedItem = PlayerManager.Instance.selectedItem;
        if(selectedItem == null) return;
        if(selectedItem is Fuel) _playerManager.AjoutCarburant(selectedItem);
        if(selectedItem is Metal) _playerManager.AjoutCoque(selectedItem);
        slotItemToMoveObj.OnMove();
        

    }
    

}
