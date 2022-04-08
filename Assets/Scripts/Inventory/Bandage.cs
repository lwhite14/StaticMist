using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class Bandage : MonoBehaviour, IItem
{
    string displayName = "BANDAGE";
    string description = "THIS SHOULD COVER ONE OF MY WOUNDS.";
    bool canUse = true;
    bool canEquip = false;

    public GameObject healSound;
    public float healValue = 1.0f;

    public void Use()
    {
        float currentHealth = FindObjectOfType<Health>().GetHealth();
        float maxHealth = FindObjectOfType<Health>().GetMaxHealth();

        if (maxHealth != currentHealth)
        {
            FindObjectOfType<Health>().Heal(healValue);
            FindObjectOfType<PlayerInventory>().inventory.RemoveItem(this);
            FindObjectOfType<PlayerInventory>().RefreshUI();
            Instantiate(healSound);
            FindObjectOfType<InventoryUI>().ResetSelection();

            SendDataToAnalytics();
        }
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

    void SendDataToAnalytics()
    {
        if (InitServices.isRecording)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "itemType", "Bandage" },
            };
            Events.CustomData("ItemUtilise", parameters);
            Events.Flush();
        }
        else
        {
            Debug.Log("Sending Event: 'ItemUtilise' with: itemType = " + "MedKit");
        }
    }
}
