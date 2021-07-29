using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

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
    }

    [SerializeField] private Object boosPrefab;
    [SerializeField] private List<GameObject> View;
    [Space(10)]
    [SerializeField] private UnityEngine.Object
        VictoirObj,
        GameOverObj,
        _SystemVide;
    [SerializeField] public StarPath
        _starPath;
    public int ActualSystemIndex;
    public planeteOrbit ActualPlanete;
    private GameObject _actualSystemObj;

    public Action lastView;

    public bool isTutoActiver = true;

    
    [Header("Game Object Reference For Prefab")]
    public GameObject WarningObj;
    

    
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && lastView != null)
        {
            if(!View.Find((obj) => obj.name == "InventaireView").activeInHierarchy)
            {
                InventaireView();

            }
            else
            {
                lastView();
            }
        }
    }

    public void InitNewGame()
    {
        if(isTutoActiver)
            Debug.Log("Salut a tous c'est le tuto");
        _starPath.NewGameNewSystem();
        GalaxyView();
    }
    
    
    public void GalaxyView()
    {
        View.ForEach((obj) => obj.SetActive(false));
        View.Find((obj) => obj.name == "Galaxy View").SetActive(true);
        View.Find((obj) => obj.name == "UIManager").SetActive(true);
        lastView = BackToGalaxyView;
        Destroy(_actualSystemObj);
        View.Remove(_actualSystemObj);
    }


    public void BackToGalaxyView()
    {
        View.ForEach((obj) => obj.SetActive(false));
        View.Find((obj) => obj.name == "Galaxy View").SetActive(true);
        View.Find((obj) => obj.name == "UIManager").SetActive(true);
        lastView = BackToGalaxyView;
    }

    public void SystemView(int SystemIndex)
    {
        
            View.ForEach((obj) => obj.SetActive(false));
            View.Find((obj) => obj.name == "UIManager").SetActive(true);

            _actualSystemObj = Instantiate(_SystemVide) as GameObject;
            _actualSystemObj.name = "SystemVide";
            View.Add(_actualSystemObj);
            foreach (var planete in _starPath.planeteSystemsList[SystemIndex].PlaneteList)
            {
                GameObject obji = Instantiate(planete);
                obji.transform.SetParent(_actualSystemObj.transform);
            }

            if (_starPath._numberOfSystem - 1 == ActualSystemIndex)
            {
                GameObject ship = (GameObject) Instantiate(boosPrefab, new Vector3(20,0,0), Quaternion.identity);
                ship.transform.SetParent(_actualSystemObj.transform);
            }
            
            lastView = BackToSystemView;
    }

    public void PlaneteView()
    {
        View.ForEach((obj) => obj.SetActive(false));
        View.Find((obj) => obj.name == "PlaneteView").SetActive(true);
        View.Find((obj) => obj.name == "UIManager").SetActive(true);
        lastView = PlaneteView;

    }

    public void BackToSystemView()
    {
        View.ForEach((obj) => obj.SetActive(false));
        View.Find((obj) => obj.name == "UIManager").SetActive(true);
        View.Find((obj) => obj.name == "SystemVide").SetActive(true);
        if (GameObject.Find("Player") != null)
        {
            var obj = GameObject.Find("Player");
            if (Vector2.Distance(Vector2.zero, obj.transform.position) > 50) 
                obj.transform.position = Vector2.zero;
        }
        lastView = BackToSystemView;
    }

    public void ExoView()
    {
        View.ForEach((obj) => obj.SetActive(false));
        View.Find((obj) => obj.name == "UIManager").SetActive(true);
        View.Find((obj) => obj.name == "exoGroundView").SetActive(true);
        lastView = ExoView;
    }

    public void GameOver()
    {
        View.ForEach((obj) => obj.SetActive(false));
        Instantiate(GameOverObj);
 
    }

    public void Victoire()
    {
        View.ForEach((obj) => obj.SetActive(false));
        Instantiate(VictoirObj);
    }

    public void InventaireView()
    {
        View.ForEach((obj) => obj.SetActive(false));
        View.Find((obj) => obj.name == "InventaireView").SetActive(true);
        PlayerManager.Instance.OnInventoryChangeCallBack?.Invoke();
    }
}
