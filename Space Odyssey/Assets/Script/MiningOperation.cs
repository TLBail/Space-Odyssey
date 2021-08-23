using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using Script.Item_and_inventory;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MiningOperation : MonoBehaviour
{
    public Slider slider;

    [SerializeField]
    public Item[] ressourceminable;

    private NotificationManager notificationManager;
    private Inventaire inventaire;
    
    private void Awake()
    {
        inventaire = GameManager.Instance.inventaire;
        notificationManager = GameManager.Instance.gameObject.GetComponent<NotificationManager>();
    }


    
    public void mineDesRessource()
    {
        gameObject.SetActive(true);
    }


    public void onLanceOperation()
    {
        if (inventaire.spaceRemaining() == 0) notificationManager.newNotification(NotificationType.INVENTAIREPLEIN);
        int nbItemRewarded = this.generateNbItemRewarded();
        if (nbItemRewarded > 0)
            inventaire.addItems(generateItems(nbItemRewarded));
    }

    private int generateNbItemRewarded()
    {
        int nbItemRewarded = ((int) slider.value) * Random.Range(1, 3);
        if (inventaire.spaceRemaining() < nbItemRewarded)
            nbItemRewarded = inventaire.spaceRemaining();
        return nbItemRewarded;
    }
    
    
    private Item[] generateItems(int nbItem)
    {
        Item[] itemsToReturn = new Item[nbItem];
        for (int i = 0; i < nbItem; i++)
        {
            itemsToReturn[i] =  ressourceminable[Random.Range(0, ressourceminable.Length)];
        }

        return itemsToReturn;
    }

    public void onClosePanel()
    {
        gameObject.SetActive(false);

    }


    
}
