using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AnimObjScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Item myItem;
    public bool isAdded;

    public Image myimage;
    public TextMeshProUGUI textelem;
    
    
    private void Start()
    {
        myimage.sprite = myItem?.icon;
        if (isAdded)
        {
            textelem.color = Color.green;
            textelem.text = "Ajout de : " + myItem?.name;
        }
        else
        {
            textelem.color = Color.red;
            textelem.text = "Supression de : " + myItem?.name;

        }
        
        Invoke("Hide" , 1f);
        
    }

    public void Hide()
    {
        Destroy(this.gameObject);
    }


}
