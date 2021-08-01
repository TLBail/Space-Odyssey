using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

public class nodeAi : MonoBehaviour
{

    [SerializeField] private GameObject hoveringInfo;
    [SerializeField] private TMP_Text ameliorationText;
    [SerializeField] public Amelioration amelioration;
    [SerializeField] private Renderer background;
    [SerializeField] private Object explosion;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    private List<Amelioration> purchasedAmeliorations;
    [NonSerialized] public NodeManager nodeManager;
    
    private void Start()
    {
        purchasedAmeliorations = PlayerManager.Instance.purchasedAmeliorations;
        hoveringInfo.SetActive(false);
        ameliorationText.SetText(amelioration.ToString());
        _spriteRenderer.sprite = amelioration.icon;
        if (purchasedAmeliorations.Contains(amelioration)) animationPurchase();
        resetState();
        nodeManager.NewAmeliorationEvent += resetState;
    }

    private void resetState()
    {
        if (!allParentAmeliorationIsPurchased)
        {
            background.material.color = new Color(0.45f, 0.44f, 0.45f);
        }
        else
        {
            background.material.color = new Color(1f, 0.98f, 1f);
        }

    }
    
    private void animationPurchase()
    {
        background.material.color = new Color(0.2f, 1f, 0f);
        Instantiate(explosion, transform);

    }

    private bool allParentAmeliorationIsPurchased => !amelioration.ParentAmeliorations.Except(purchasedAmeliorations).Any();
    
    private void OnMouseDown()
    {
        if(purchasedAmeliorations.Contains(amelioration)) return;
        if (!purchasedAmeliorations.Contains(amelioration) && allParentAmeliorationIsPurchased) tryTopurchaseAmelioration();
    }

    private void tryTopurchaseAmelioration()
    {
        if(amelioration.itemsCost.Except(PlayerManager.Instance.Inventaire).Any()) return;
        foreach (Item item in amelioration.itemsCost) PlayerManager.Instance.RemoveItem(item);
        purchasedAmeliorations.Add(amelioration);
        nodeManager.NewAmeliorationEvent -= resetState;
        nodeManager.invokeNewAmelioration();
        animationPurchase();

    }
    
    private void OnMouseEnter()
    {
        hoveringInfo.SetActive(true);
    }

    private void OnMouseExit()
    {
        hoveringInfo.SetActive(false);
    }
    
}
