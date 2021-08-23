using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

public class NodeManager : MonoBehaviour
{
    [SerializeField] private Object nodePrefab;
    
    [SerializeField] private Object linePrefab;
    
    [SerializeField] private int xBonus;
    [SerializeField] private int yBonus;
    
    private Dictionary<Amelioration, GameObject> nodeDictionary;

    public event Action NewAmeliorationEvent;
    private Amelioration[] _ameliorationses;
    

    private void Awake()
    {
        nodeDictionary = new Dictionary<Amelioration, GameObject>();
        _ameliorationses = AmeliorationManager.Instance.ameliorations;
    }

    public void invokeNewAmelioration()
    {
        NewAmeliorationEvent?.Invoke();
    }

    
    
    private void Start()
    {
        createNode();
        createLinesBetweenNode();
    }

    private void createNode()
    {
        Vector3 positionNode = Vector3.zero;
        foreach (Amelioration amelioration in _ameliorationses)
        {
            positionNode.y = depthLevel(amelioration) * yBonus;
            GameObject inst = (GameObject) Instantiate(nodePrefab, positionNode, Quaternion.Euler(Vector3.zero));
            inst.GetComponent<nodeAi>().amelioration = amelioration;
            inst.GetComponent<nodeAi>().nodeManager = this;
            positionNode.x += xBonus;
            inst.transform.SetParent(transform);
            
            
            nodeDictionary.Add(amelioration, inst);
        }
    }

    private int depthLevel(Amelioration amelioration, int depth = 0)
    {
        if (!amelioration.ParentAmeliorations.Any()) return depth;
        int max = 0;
        Amelioration ameliorationToReturn = amelioration.ParentAmeliorations[0];
        foreach (Amelioration amelio in amelioration.ParentAmeliorations)
        {
            int i = depthLevel(amelio, depth + 1);
            if (i > max)
            {
                max = i;
                ameliorationToReturn = amelio;
            }
        }
        return depthLevel(ameliorationToReturn, depth + 1);
    }
    
    private void createLinesBetweenNode()
    {

        foreach (Amelioration amelioration in _ameliorationses)
        {
            if (amelioration.ParentAmeliorations.Any())
            {
                Vector3 posOfChild = nodeDictionary[amelioration].transform.position;
                Vector3 posOfParent = nodeDictionary[amelioration.ParentAmeliorations[0]].transform.position;
                createNewLine(posOfChild, posOfParent);
                
            }   
        }
    }


    private void createNewLine(Vector3 startPos, Vector3 endPos)
    {
        //on augmente le z des pos sinon sa se superpose
        startPos.z = 1;
        endPos.z = 1;
        
        GameObject hopobj = (GameObject) Instantiate(linePrefab);
        hopobj.transform.SetParent(transform);
        LineRenderer _lineRenderer = hopobj.GetComponent<LineRenderer>();
        
        _lineRenderer.SetPosition(0, startPos);
        _lineRenderer.SetPosition(1, endPos);

    }
}
