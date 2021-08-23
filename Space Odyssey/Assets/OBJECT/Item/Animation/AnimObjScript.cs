using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimObjScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    private Item myItem;
    private bool isAdded;
    private String text;
    
    public Image myimage;
    public TextMeshProUGUI textelem;

    public void setAnim(Item item, bool isAdded)
    {
        setAnim(item, isAdded, item.name);
    }

    public void setAnim(Item item, bool isAdded, String text)
    {
        myItem = item;
        this.isAdded = isAdded;
        this.text = text;
    }
    
    private void Start()
    {
        myimage.sprite = myItem?.icon;
        if (isAdded)
        {
            textelem.color = Color.green;
            textelem.text = "Ajout de : " + text;
        }
        else
        {
            textelem.color = Color.red;
            textelem.text = "Supression de : " + text;

        }
        
        Invoke("Hide" , 1f);
        
    }

    public void Hide()
    {
        Destroy(this.gameObject);
    }


}
