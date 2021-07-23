using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script;
using UnityEngine;
public class PlayerManager : MonoBehaviour
{

    public static PlayerManager Instance { get; private set; }

    public delegate void OnInventoryChange();
    public OnInventoryChange OnInventoryChangeCallBack;
    
    private Queue<AnimItem> inventoryAction = new Queue<AnimItem>();
    [SerializeField] public GameObject animItemObj,animInventairPleinObj;
    [SerializeField] public Animation animSliderEnergie;
    [SerializeField] public Animation animSliderVie;
    public Item selectedItem;
    public SlotItemToMove SlotItemToMove;
    public bool isOcuped = false;
    private bool IsOnAnimationInvantaire = false;

    public GameObject shipGameObject;

    
    public NotificationManager notificationManager;
    
    private void Awake()
    {
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
                GameManager.Instance.GameOver();
            }

            if (Energie <= 0)
            {
                GameManager.Instance.GameOver();
            }
        }
    }


    public void AjoutCarburant(Item fuelItem)
    {
        if (!(fuelItem is Fuel fuel)) return;
        if (Energie < MaxEnergie)
        {
            Energie += fuel.amountOfFuel;
            RemoveItem(fuel);
        
            /*
            Debug.Log("Plus de Carburant");
            notificationManager.newNotification(NotificationType.INFORMATION, "plus de carburant a disposition");
            */
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
            RemoveItem(coque);
        }
        else
        {
            animSliderVie.PlayQueued("full animation");
        }
    }

    
    
    private GameObject objtoDelete;
    public bool AddItem(Item item)
    {
        if (Inventaire.Count >= MaxItemSpace)
        {
            notificationManager.newNotification(NotificationType.INVENTAIREPLEIN);
            return false;
        }
        else
        {
            Inventaire.Add(item);
            
            notificationManager.newNotification(new AnimItem(item, true));
            OnInventoryChangeCallBack?.Invoke();
            return true;

        }
    }

    public bool RemoveItem(Item item)
    {
        if (Inventaire.Find((itema) => itema.name == item.name))
        {
            Debug.Log("Supression de " + item.name);
            Item oldItem = ScriptableObject.CreateInstance<Item>();
            oldItem.name = item.name;
            oldItem.icon = item.icon;
            
            notificationManager.newNotification(new AnimItem(oldItem, false));

            Inventaire.Remove(item);
            OnInventoryChangeCallBack?.Invoke();
            return true;
        }
        else
        {
            Debug.Log("Echec de la supresssion de l'objet");
            return false;

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
    [Space(10)]
    public List<Item> Inventaire;
    public int MaxItemSpace;
}