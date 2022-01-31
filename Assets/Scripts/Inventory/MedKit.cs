using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedKit : MonoBehaviour, IItem
{
    string displayName = "MED KIT";
    string description = "THIS SHOULD PATCH ME UP IF I GET HURT.";
    bool canUse = true;
    bool canEquip = false;
    bool canReload = false;

    bool runningRoutine = false;

    public void Use() 
    {
    
    }

    public void Examine(Text examineText) 
    {
        FindObjectOfType<CoroutineHelper>().HelperStartExamining(description, examineText);
    }

    public void Equip() { }

    public void Reload() { }

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
