using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour, IItem
{
    string displayName = "FLASHLIGHT";
    string description = "A FLASHLIGHT... TO HELP ME SEE...";
    bool canUse = false;
    bool canEquip = true;
    bool canReload = false;

    bool runningRoutine = false;

    public void Use() { }

    public void Examine(Text examineText)
    {
        FindObjectOfType<CoroutineHelper>().HelperStartExamining(description, examineText);
    }

    public void Equip() 
    {
        Debug.Log("*Equip the flashlight*");
    }

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
