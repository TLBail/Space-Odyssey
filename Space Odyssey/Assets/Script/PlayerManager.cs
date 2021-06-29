using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            if (!IsOnAnimationInvantaire)
            {
                IsOnAnimationInvantaire = true;
                objtoDelete = Instantiate(animInventairPleinObj);
                Invoke("StopAnimObj", 2f);
            }
            return false;
        }
        else
        {
            Inventaire.Add(item);
            inventoryAction.Enqueue(new AnimItem(item, true));
            QueuManager();
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
            inventoryAction.Enqueue(new AnimItem(oldItem, false));
            QueuManager();
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

   

    public void QueuManager()
    {
        while (inventoryAction.Count != 0 && !isOcuped)
        {

            AddRemoveAnimation(inventoryAction.Dequeue());
        }

    }

    public void AddRemoveAnimation(AnimItem elem)
    {
        isOcuped = true;
        Invoke("StopAnim", 1f); 
        GameObject animObjelem = Instantiate(animItemObj);
        animObjelem.GetComponent<AnimObjScript>().myItem = elem.Item;
        animObjelem.GetComponent<AnimObjScript>().isAdded = elem.isAdded;
    }
    


    public void StopAnim()
    {
        isOcuped = false;
        QueuManager();
    }


    public void StopAnimObj()
    {
        IsOnAnimationInvantaire = false;
        Destroy(objtoDelete);
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


    
    public class AnimItem
    {
        public Item Item;
        public bool isAdded;

        public AnimItem(Item newItem, bool newIsAdded)
        {
            Item = newItem;
            isAdded = newIsAdded;
        }

    }
}