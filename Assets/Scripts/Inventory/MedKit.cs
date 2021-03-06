using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class MedKit : MonoBehaviour, IItem
{
    string displayName = "MED KIT";
    string description = "THIS SHOULD PATCH ME UP IF I GET HURT."; 
    bool canUse = true;
    bool canEquip = false;

    public GameObject healSound;
    public float healValue = 2.0f;

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

            AnalyticsFunctions.ItemUtilise("MedKit");
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
}
