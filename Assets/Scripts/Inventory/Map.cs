using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class Map : MonoBehaviour, IItem
{
    string displayName = "MAP";
    string description = "I MIGHT BE ABLE TO FIND SOME HIDDEN SPOTS IN THIS MAP.";
    bool canUse = true;
    bool canEquip = false;

    public Sprite map;

    public void Use()
    {
        FindObjectOfType<InventoryUI>().ViewMap(map);

        AnalyticsFunctions.ItemUtilise("Map");
    }

    public void Examine()
    {
        FindObjectOfType<CoroutineHelper>().HelperStopCoroutine();
        FindObjectOfType<CoroutineHelper>().HelperStartExamining(description);
    }

    public void Equip() { }


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

    public string GetName()
    {
        return displayName;
    }
    public string GetDescription()
    {
        return description;
    }
}
