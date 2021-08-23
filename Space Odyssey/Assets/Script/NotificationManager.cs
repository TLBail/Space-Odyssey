using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Script;
using Script.Item_and_inventory;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

public class NotificationManager : MonoBehaviour
{

    private Queue<AnimItem> animQueue= new Queue<AnimItem>();

    
    [SerializeField] public Object animItemObj,animInventairPleinObj,animInformationObj, popUpInfoObj;

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
            GameObject objToDelete = (GameObject) Instantiate(animInventairPleinObj);
            StartCoroutine(stopAnimObj(objToDelete, 2f));
        }
    }

    public void newNotification(NotificationType notificationType, String textToDisplay)
    {
        if (notificationType == NotificationType.INFORMATION)
        {
            GameObject objToDelete = (GameObject) Instantiate(animInformationObj);
            objToDelete.GetComponent<objetStore>().objects[0].GetComponent<TextMeshProUGUI>().SetText(textToDisplay);
            StartCoroutine(stopAnimObj(objToDelete, 2f));
        }

        
    }

    public GameObject newNotification(NotificationType notificationType, String title, String text)
    {
        GameObject notif = null;
        if (notificationType == NotificationType.TEXT)
        {
            notif = (GameObject) Instantiate(popUpInfoObj);
            TextMeshProUGUI titleComponent =
                notif.GetComponent<objetStore>().objects[0].GetComponent<TextMeshProUGUI>();
            titleComponent.SetText(title);
            TextMeshProUGUI textComponent =
                notif.GetComponent<objetStore>().objects[1].GetComponent<TextMeshProUGUI>();
            textComponent.SetText(text);

        }

        return notif;

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
        GameObject animObjelem = (GameObject) Instantiate(animItemObj);
        if (elem is AnimItems animItems) animObjelem.GetComponent<AnimObjScript>().setAnim(
            elem.Item, elem.isAdded,animItems.Item.name +" x" + animItems.nbItem);
        else animObjelem.GetComponent<AnimObjScript>().setAnim(elem.Item, elem.isAdded);
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
