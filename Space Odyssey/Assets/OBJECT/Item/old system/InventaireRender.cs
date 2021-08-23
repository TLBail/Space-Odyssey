using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventaireRender : MonoBehaviour
{
    [SerializeField] private DragAndDropCell[] draganddropArray;

    
    private void Awake()
    {
        draganddropArray = GetComponentsInChildren<DragAndDropCell>();
    }

    private void OnEnable()
    {
        /*
        for (int i = 0; i < PlayerManager.Instance.Inventaire.Count; i++)
        {
            if(PlayerManager.Instance.Inventaire[i].itemInCell != null)
            {
                GameObject obj = Instantiate(PlayerManager.Instance.Inventaire[i].itemInCell) as GameObject;
                DragAndDropItem dadI = obj.GetComponent<DragAndDropItem>();
                draganddropArray[i].AddItem(dadI);
            }
        }
        */
        
    }
}
