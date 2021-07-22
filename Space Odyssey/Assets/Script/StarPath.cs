using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class StarPath : MonoBehaviour
{
    [SerializeField] public float _numberOfSystem, _nombreDePlaneteMin, _nombreDePlaneteMax;
    [FormerlySerializedAs("_OrbitXmin")] [SerializeField] public float orbitXmin;
    [SerializeField] public float _OrbitXmax, _OrbitYmin, _OrbitYmax;
    [SerializeField] public List<PlaneteSystem> planeteSystemsList = new List<PlaneteSystem>();
    [SerializeField] private GameObject[] _planeteBank;
    [SerializeField] private GameObject _stars, finalStars, main, transitionObject;

    
    
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
                GameObject obj;
                do
                {
                    obj = _planeteBank[Random.Range(0, _planeteBank.Length)];
                } while (planeteSystemsList[x].PlaneteList.Contains(obj));
                
                obj.transform.position = new Vector3(Random.Range(orbitXmin, _OrbitXmax), Random.Range(_OrbitYmin , _OrbitYmax), 23);
                planeteSystemsList[x].PlaneteList.Add(obj);
            }

        }
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
            Debug.Log(obj.GetComponent<StarClick>().main);
            Debug.Log(obj.GetComponent<StarClick>().transitionObject);
        }
        posx += Random.Range(5, 15);
        posy += Random.Range(-5, -2);
        GameObject finalstarObj = Instantiate(finalStars, new Vector3(posx, posy, 0), transform.rotation);
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