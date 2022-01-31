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

    public GameObject flashlight;
    bool isEquiped = false;

    public void Use() { }

    public void Examine()
    {
        FindObjectOfType<CoroutineHelper>().HelperStartExamining(description);
    }

    public void Equip() 
    {
        if (!isEquiped)
        {
            Instantiate(flashlight, GameObject.Find("ViewmodelTargetPos").transform.position, Quaternion.identity);
            isEquiped = true;
        }
        else 
        {
            FindObjectOfType<CoroutineHelper>().HelperStartExamining("I'M ALREADY USING THIS.");
        }
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
