using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour, IItem
{
    string displayName = "KEY";
    string description = "THIS SHOULD OPEN THE GATE TO GET AWAY FROM THIS PLACE...";
    bool canUse = false;
    bool canEquip = false;
    bool canReload = false;

    public void Use()
    {

    }

    public void Examine()
    {
        FindObjectOfType<CoroutineHelper>().HelperStartExamining(description);
    }

    public void Equip() 
    {
    
    }

    public void Reload() 
    { 
    
    }

    public GameObject GetInvIcon() 
    {
        return gameObject;
    }

    public bool GetCanUse() 
    {
        return canUse;
    }

    public bool GetCanEquip() 
    { 
        return canEquip; 
    }

    public bool GetCanReload() 
    { 
        return canReload; 
    }

    public string GetName() 
    { 
        return displayName; 
    }
    public string GetDescription() 
    {
        return description;
    }
}
