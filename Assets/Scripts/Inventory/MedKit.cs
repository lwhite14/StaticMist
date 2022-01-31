using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedKit : MonoBehaviour, IItem
{
    string displayName = "MED KIT";
    string description = "THIS SHOULD PATCH ME UP IF I GET HURT."; 
    int InventorySpace = -1;
    bool canUse = true;
    bool canEquip = false;
    bool canReload = false;

    public GameObject healSound;

    public void Use() 
    {
        float currentHealth = FindObjectOfType<Health>().GetHealth();
        float maxHealth = FindObjectOfType<Health>().GetMaxHealth();

        if (maxHealth != currentHealth)
        {
            FindObjectOfType<Health>().Heal(2.0f);
            FindObjectOfType<PlayerInventory>().inventory.RemoveItem(this);
            FindObjectOfType<PlayerInventory>().RefreshUI();
            Instantiate(healSound);
        }
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

    public int GetInventorySpace()
    {
        return InventorySpace;
    }
}
