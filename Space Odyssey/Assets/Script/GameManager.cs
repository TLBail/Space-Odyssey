using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Script.Item_and_inventory;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance { get;private set; }


    void Awake()
    {
        if (Instance == null) {
            Instance = this;
        } else {
            Debug.Log("Warning: multiple " + this + " in scene!");
        }

        inventaire = gameObject.GetComponent<Inventaire>();
    }

    [SerializeField] private Object boosPrefab;
    [SerializeField] private List<GameObject> views;
    [Space(10)]
    [SerializeField] private UnityEngine.Object
        victoirObj,
        gameOverObj,
        systemVide;
    [SerializeField] public StarPath
        starPath;
    [NonSerialized] public int actualSystemIndex = -1;
    public planeteOrbit actualPlanete;
    private GameObject _actualSystemObj;

    public Action lastView;

    public bool isTutoActiver = true;
    
    [Header("Game Object Reference For Prefab")]
    public GameObject WarningObj;

    public Inventaire inventaire;
    
    
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && lastView != null)
        {
            if(!views.Find((obj) => obj.name == "InventaireView").activeInHierarchy)
            {
                inventaireView();

            }
            else
            {
                lastView();
            }
        }
    }

    public void initNewGame()
    {
        if(isTutoActiver)
            Debug.Log("Salut a tous c'est le tuto");
        starPath.NewGameNewSystem();
        galaxyView();
    }
    
    
    public void galaxyView()
    {
        views.ForEach((obj) => obj.SetActive(false));
        views.Find((obj) => obj.name == "Galaxy View").SetActive(true);
        views.Find((obj) => obj.name == "UIManager").SetActive(true);
        lastView = backToGalaxyView;
        Destroy(_actualSystemObj);
        views.Remove(_actualSystemObj);
    }


    public void backToGalaxyView()
    {
        views.ForEach((obj) => obj.SetActive(false));
        views.Find((obj) => obj.name == "Galaxy View").SetActive(true);
        views.Find((obj) => obj.name == "UIManager").SetActive(true);
        lastView = backToGalaxyView;
    }

    public void systemView(int SystemIndex)
    {
        
            views.ForEach((obj) => obj.SetActive(false));
            views.Find((obj) => obj.name == "UIManager").SetActive(true);

            _actualSystemObj = Instantiate(systemVide) as GameObject;
            _actualSystemObj.name = "SystemVide";
            views.Add(_actualSystemObj);
            foreach (var planete in starPath.planeteSystemsList[SystemIndex].PlaneteList)
            {
                GameObject obji = Instantiate(planete);
                obji.transform.SetParent(_actualSystemObj.transform);
            }

            if (starPath._numberOfSystem - 1 == actualSystemIndex)
            {
                GameObject ship = (GameObject) Instantiate(boosPrefab, new Vector3(20,0,0), Quaternion.identity);
                ship.transform.SetParent(_actualSystemObj.transform);
            }
            
            lastView = backToSystemView;
    }

    public void planeteView()
    {
        views.ForEach((obj) => obj.SetActive(false));
        views.Find((obj) => obj.name == "PlaneteView").SetActive(true);
        views.Find((obj) => obj.name == "UIManager").SetActive(true);
        lastView = planeteView;

    }

    public void backToSystemView()
    {
        views.ForEach((obj) => obj.SetActive(false));
        views.Find((obj) => obj.name == "UIManager").SetActive(true);
        views.Find((obj) => obj.name == "SystemVide").SetActive(true);
        replaceThePlayerIfOutOfBound();
        actualPlanete = null;
        lastView = backToSystemView;
    }

    private void replaceThePlayerIfOutOfBound()
    {
        GameObject playerShip = getPlayerShip();
        if(playerOutOfBound(playerShip)) playerShip.transform.position = Vector2.zero;
    }

    private GameObject getPlayerShip()
    {
        PlayerManager playerManager = PlayerManager.Instance;
        GameObject playerShip = playerManager.shipGameObject;
        if (playerShip == null) throw new Exception("no player ship obj registered");
        return playerShip;
    }
    
    private bool playerOutOfBound(GameObject playerShip) => Vector2.Distance(Vector2.zero, playerShip.transform.position) > 50;

    public void exoView()
    {
        views.ForEach((obj) => obj.SetActive(false));
        views.Find((obj) => obj.name == "UIManager").SetActive(true);
        views.Find((obj) => obj.name == "exoGroundView").SetActive(true);
        lastView = exoView;
    }

    public void gameOver()
    {
        views.ForEach((obj) => obj.SetActive(false));
        Instantiate(gameOverObj);
 
    }

    public void victoire()
    {
        views.ForEach((obj) => obj.SetActive(false));
        Instantiate(victoirObj);
    }

    public void inventaireView()
    {
        views.ForEach((obj) => obj.SetActive(false));
        views.Find((obj) => obj.name == "InventaireView").SetActive(true);
        inventaire.OnInventoryChangeCallBack?.Invoke();
    }
}
