using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [HideInInspector]
    public GameObject currentInvIcon = null;

    public void Refresh() 
    {
        if (currentInvIcon != null) 
        {
            Instantiate(currentInvIcon, gameObject.transform.GetChild(0));
        }
    }
}
