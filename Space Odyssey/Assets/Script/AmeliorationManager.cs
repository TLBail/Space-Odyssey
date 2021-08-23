using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script.Item_and_inventory;
using UnityEngine;

public class AmeliorationManager : MonoBehaviour
{

    public static AmeliorationManager Instance { get; private set; }

    private Inventaire inventaire;
    
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Debug.Log("Warning : multiplie AmeliorationManager in scene !");
        inventaire = GameManager.Instance.gameObject.GetComponent<Inventaire>();

    }


    [SerializeField] public Amelioration[] ameliorations;
    [SerializeField] public List<Amelioration> ameliorationsAchete;

    
    
    public bool buyAmelioration(Amelioration amelioration)
    {
        if(!inventaire.isOwningFollowingItems(amelioration.itemsCost)) return false;
        foreach (Item item in amelioration.itemsCost) inventaire.removeItem(item);
        ameliorationsAchete.Add(amelioration);
        amelioration.effect.Execute();
        return true;
    }


}
