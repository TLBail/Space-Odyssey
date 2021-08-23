using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script;
using Script.Item_and_inventory;
using UnityEngine;
public class PlayerManager : MonoBehaviour
{

    public static PlayerManager Instance { get; private set; }
    
    [SerializeField] public Animation animSliderEnergie;
    [SerializeField] public Animation animSliderVie;
    public Item selectedItem;
    public SlotItemToMove SlotItemToMove;
    private bool IsOnAnimationInvantaire = false;

    public GameObject shipGameObject;

    
    public NotificationManager notificationManager;

    private Inventaire inventaire;
    
    private void Awake()
    {
        inventaire = gameObject.GetComponent<Inventaire>();
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Warning: multiple " + this + " in scene!");
        }
    }


    private void Start()
    {
        Vie = VieInit;
        Energie = EnergieInit;
    }

    private void Update()
    {
        if (GameObject.Find("Game Over(Clone)") == null)
        {

            if (Vie <= 0)
            {
                GameManager.Instance.gameOver();
            }

            if (Energie <= 0)
            {
                GameManager.Instance.gameOver();
            }
        }
    }


    public void AjoutCarburant(Item fuelItem)
    {
        if (!(fuelItem is Fuel fuel)) return;
        if (Energie < MaxEnergie)
        {
            Energie += fuel.amountOfFuel;
            inventaire.removeItem(fuel);
        }
        else
        {
            animSliderEnergie.PlayQueued("full animation");
        }
    }
    
    
    public void AjoutCoque(Item CoqueItem)
    {
        if (!(CoqueItem is Metal coque)) return;
        
        if (Vie < MaxVie)
        {
            Vie += coque.amoutOfVie;
            inventaire.removeItem(coque);
        }
        else
        {
            animSliderVie.PlayQueued("full animation");
        }
    }

    
    [Header("Player Carcateristique")]
    public float Vie;
    public float MaxVie;
    public float VieInit;
    [Space(10)]
    public float Energie;
    public float MaxEnergie;
    public float EnergieInit;

}