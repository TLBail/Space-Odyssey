using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class StarPath : MonoBehaviour
{
    [SerializeField] public float _numberOfSystem, _nombreDePlaneteMin, _nombreDePlaneteMax;
    [SerializeField] public float orbitmax, orbitmin;
    [SerializeField] public List<PlaneteSystem> planeteSystemsList = new List<PlaneteSystem>();
    [SerializeField] private GameObject[] _planeteBank;
    [SerializeField] private GameObject _stars, finalStars, main, transitionObject;

    const int ZPOSITIONPLANETE = 22;
    
    public void NewGameNewSystem()
    {
        CreateNewSystem();
        DisplayNewSystem();
    }

    public void CreateNewSystem()
    {
        for (int x = 0; x < _numberOfSystem; x++)
        {
            float planeteNombre = (int)Random.Range(_nombreDePlaneteMin, _nombreDePlaneteMax);
            planeteSystemsList.Add(new PlaneteSystem(planeteNombre));

            for (int i = 0; i < planeteNombre; i++)
            {
                GameObject planeteGOBJ;
                do
                {
                    planeteGOBJ = _planeteBank[Random.Range(0, _planeteBank.Length)];
                } while (planeteSystemsList[x].PlaneteList.Contains(planeteGOBJ));
                planeteGOBJ.transform.position = newPlanetePosition();
                planeteSystemsList[x].PlaneteList.Add(planeteGOBJ);
            }

        }
    }


    private Vector3 newPlanetePosition()
    {
        Vector3 vector3;
        vector3 =  new Vector3(Random.Range(orbitmin, orbitmax), Random.Range(orbitmin , orbitmax), ZPOSITIONPLANETE);
        int min = 0;
        int max = 2;
        if (Random.Range(min, max) == 1) vector3.x = -vector3.x;
        if (Random.Range(min, max) == 1) vector3.y = -vector3.y;
        return vector3;
    }
    

    public void DisplayNewSystem()
    {
        int posx = -14, posy = 6;
        for (int i = 0; i < planeteSystemsList.Count; i++)
        {
            posx += Random.Range(5, 15);
            posy += Random.Range(-5, -2);
            GameObject obj = Instantiate(_stars, new Vector3(posx, posy, 10), transform.rotation);
            obj.transform.SetParent(gameObject.transform);
            obj.GetComponent<StarClick>().SystemIndex = i;
            obj.GetComponent<StarClick>().CoutCarbu = (int)Random.Range(PlayerManager.Instance.MaxEnergie/4 , PlayerManager.Instance.MaxEnergie);
            obj.GetComponent<StarClick>().main = main;
            obj.GetComponent<StarClick>().transitionObject = transitionObject;
        }
        posx += Random.Range(5, 15);
        posy += Random.Range(-5, -2);
        GameObject finalstarObj = Instantiate(finalStars, new Vector3(posx, posy, 10), transform.rotation);
        finalstarObj.transform.SetParent(gameObject.transform);
        finalstarObj.GetComponent<StarClick>().SystemIndex = planeteSystemsList.Count;
        finalstarObj.GetComponent<StarClick>().CoutCarbu = (int)Random.Range(PlayerManager.Instance.MaxEnergie/4 , PlayerManager.Instance.MaxEnergie);
        finalstarObj.GetComponent<StarClick>().main = main;
        finalstarObj.GetComponent<StarClick>().transitionObject = transitionObject;

    }
    
}

[System.Serializable]
public class PlaneteSystem
{
    public float NombreDePlanete;
    public List<GameObject> PlaneteList = new List<GameObject>();

    public PlaneteSystem(float _nombreDeplanete)
    {
        NombreDePlanete = _nombreDeplanete;
    }
}