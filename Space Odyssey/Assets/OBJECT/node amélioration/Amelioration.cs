using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
[CreateAssetMenu(fileName = "New Amelioration", menuName = "Inventory/Amelioration")]
public class Amelioration : ScriptableObject 
{

    public String name = "new ameliorations";
    public Sprite icon = null;

    public Effect effect;

    public Amelioration[] ParentAmeliorations;

    public Item[] itemsCost;

    
    public override string ToString()
    {
        String sum = "";
        foreach (Item item in itemsCost)
        {
            sum += item.name + ",";
        }
        return name + " coût : \n (" + sum + ")";
    }
}


public enum Effect
{
    VIELVL1,
    VIELVL2,
    VIELVL3,
    ENERGIELVL1,
    ENERGIELVL2,
    ENERGIELVL3
    

    
}

public static class EffectExecute
{
    
    public static void Execute(this Effect effect)
    {
        switch (effect)
        {
            case Effect.VIELVL1:
                PlayerManager.Instance.MaxVie = PlayerManager.Instance.MaxVie * 1.5f;                
            break;
            case Effect.VIELVL2:
                PlayerManager.Instance.MaxVie = PlayerManager.Instance.MaxVie * 2f;                
            break;
            case Effect.VIELVL3:
                PlayerManager.Instance.MaxVie = PlayerManager.Instance.MaxVie * 4f;                
            break;
            case Effect.ENERGIELVL1:
                PlayerManager.Instance.MaxEnergie = PlayerManager.Instance.MaxEnergie * 1.5f;                
            break;
            case Effect.ENERGIELVL2:
                PlayerManager.Instance.MaxEnergie = PlayerManager.Instance.MaxEnergie * 2f;                
            break;
            case Effect.ENERGIELVL3:
                PlayerManager.Instance.MaxEnergie = PlayerManager.Instance.MaxEnergie * 4f;                
            break;
            
                
        }
    }
}
