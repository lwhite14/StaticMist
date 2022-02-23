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
    bool canReload = false;

    Sprite map;

    public void Use()
    {
        FindObjectOfType<InventoryUI>().ViewMap(map);

        SendDataToAnalytics();
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

    public void SetMapImage(Sprite newMap) 
    {
        map = newMap;
    }

    void SendDataToAnalytics()
    {
        if (InitServices.isRecording)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "itemType", "Map" },
            };
            Events.CustomData("ItemUtilise", parameters);
            Events.Flush();
        }
        else
        {
            Debug.Log("Sending Event: 'ItemUtilise' with: itemType = " + "Map");
        }
    }
}
