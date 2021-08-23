using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class MenuManager : MonoBehaviour
{

    // Start is called before the first frame update
    [SerializeField] private  String title;
    [SerializeField, TextArea] private  String text;
    
    
    void Start()
    {
        NotificationManager notificationManager = PlayerManager.Instance.notificationManager;
        notificationManager.newNotification(NotificationType.TEXT, title, text);
        
    }
    

}
