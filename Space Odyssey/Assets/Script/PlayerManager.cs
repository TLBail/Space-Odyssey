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
    public Item selectedItem;
    public SlotItemToMove SlotItemToMove;
    public bool isOcuped = false;
    private bool IsOnAnimationInvantaire = false;

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

    public void MineRessource()
    {
        Item newItem = ressourceminable[Random.Range(0, ressourceminable.Length)];
        bool isadded = AddItem(newItem);
    }

    public void AjoutCarburant()
    {
        if (Energie < MaxEnergie)
        {
            Item fuel = Inventaire.Find((item) => item.isFuel == true);
            if (fuel != null)
            {
                Energie += fuel.AmountOfFuel;
                RemoveItem(fuel);
            }
            else
            {
                Debug.Log("Plus de Carburant");
                notificationManager.newNotification(NotificationType.INFORMATION, "plus de carburant a disposition");
            }
        }
        else
        {
            animSliderEnergie.PlayQueued("full animation");
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
    public Item[] ressourceminable;

    
}