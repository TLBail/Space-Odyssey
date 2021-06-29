using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using TMPro;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{

    private Queue<AnimItem> animQueue= new Queue<AnimItem>();

    
    [SerializeField] public GameObject animItemObj,animInventairPleinObj,animInformationObj;

    private bool isAnimating;
    private bool isAnimatingInventaireFull;
    
    
    private void Awake()
    {
        isAnimating = false;
    }


    public void newNotification(AnimItem animItem)
    {
        animQueue.Enqueue(animItem);
        updateQueue();
        
    }

    public void newNotification(NotificationType notificationType)
    {

        if (notificationType == NotificationType.INVENTAIREPLEIN && !isAnimatingInventaireFull)
        {
            isAnimatingInventaireFull = true;
            GameObject objToDelete = Instantiate(animInventairPleinObj);
            StartCoroutine(stopAnimObj(objToDelete, 2f));
        }
    }

    public void newNotification(NotificationType notificationType, String textToDisplay)
    {
        if (notificationType == NotificationType.INFORMATION)
        {
            GameObject objToDelete = Instantiate(animInformationObj);
            objToDelete.GetComponent<objetStore>().myObject.GetComponent<TextMeshProUGUI>().SetText(textToDisplay);
            StartCoroutine(stopAnimObj(objToDelete, 2f));
        }
    }

    public void updateQueue()
    {
        if (animQueue.Count > 0 && !isAnimating)
        {
            startAnimation(animQueue.Dequeue());
        }
    }
    

    private void startAnimation(AnimItem elem)
    {
        isAnimating = true;
        Invoke("StopAnim", 1f); 
        GameObject animObjelem = Instantiate(animItemObj);
        animObjelem.GetComponent<AnimObjScript>().myItem = elem.Item;
        animObjelem.GetComponent<AnimObjScript>().isAdded = elem.isAdded;
    }
    


    public void StopAnim()
    {
        isAnimating = false;
        updateQueue();
    }

    IEnumerator stopAnimObj(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        isAnimatingInventaireFull = false;
        Destroy(gameObject);
    }

}
