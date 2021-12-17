using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject[] itemSlots = new GameObject[16];
    public Button useButton;
    public Button examineButton;
    public Button equipButton;
    public Button reloadButton;
    public Text nameText;
    public Text descriptionText;
    GameObject viewedItem = null;
    Animator anim;
    bool isOn = false;
    bool isDead = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void InventoryInput() 
    {
        if (!isDead)
        {
            if (!isOn)
            {
                anim.SetBool("isOn", true);
                FindObjectOfType<MouseLook>().SetCursorMode(false);
                FindObjectOfType<MouseLook>().SetIsInMenu(true);
                FindObjectOfType<PlayerMovement>().SetIsInMenu(true);
                isOn = true;
            }
            else
            {
                anim.SetBool("isOn", false);
                FindObjectOfType<MouseLook>().SetCursorMode(true);
                FindObjectOfType<MouseLook>().SetIsInMenu(false);
                FindObjectOfType<PlayerMovement>().SetIsInMenu(false);
                isOn = false;
            }
        }
    }

    public void RefreshUI(List<IItem> items) 
    {
        for (int i = 0; i < items.Count; i++)
        {
            itemSlots[i].GetComponent<ItemSlot>().currentItem = items[i].GetInvIcon();
            itemSlots[i].GetComponent<ItemSlot>().Refresh();
        }
    }

    public void SetIsDead(bool newIsDead) 
    {
        isDead = newIsDead;
    }

    public void SetViewedItem(GameObject newViewedItem) 
    {
        viewedItem = newViewedItem;
        if (viewedItem != null)
        {
            useButton.interactable = viewedItem.GetComponent<IItem>().GetCanUse();
            equipButton.interactable = viewedItem.GetComponent<IItem>().GetCanEquip();
            reloadButton.interactable = viewedItem.GetComponent<IItem>().GetCanReload();
            nameText.text = viewedItem.GetComponent<IItem>().GetName();
            examineButton.interactable = true;
        }
        else 
        {
            useButton.interactable = false;
            equipButton.interactable = false;
            reloadButton.interactable = false;
            nameText.text = "SELECT AN ITEM";
            descriptionText.text = "";
            examineButton.interactable = false;
        }

    }

    public GameObject GetViewedItem() 
    { 
        return viewedItem; 
    }

    public void Use() 
    { 
    
    }

    public void Examine() 
    { 
    
    }

    public void Equip() 
    { 
    
    }

    public void Reload() 
    { 
    
    }
}
