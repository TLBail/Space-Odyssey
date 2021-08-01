using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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

public static class Extensions
{
    public static void Execute(this Effect effect)
    {
        switch (effect)
        {
            case Effect.VIELVL1:
                //rajout au joueur de plus de vie
            break;
                
        }
    }
}
