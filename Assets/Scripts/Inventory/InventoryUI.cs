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
    public MapDisplayer mapDisplayer;
    GameObject viewedItem = null;
    Animator anim;
    bool isOn = false;
    bool canUse = true;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void InventoryInput() 
    {
        if (canUse)
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
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (i < items.Count)
            {
                itemSlots[i].GetComponent<ItemSlot>().currentItem = items[i].GetInvIcon();
            }
            else
            {
                itemSlots[i].GetComponent<ItemSlot>().currentItem = null;
            }
            itemSlots[i].GetComponent<ItemSlot>().Refresh();
        }
    }

    public void SetCanUse(bool newCanUse) 
    {
        canUse = newCanUse;
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
            FindObjectOfType<CoroutineHelper>().HelperStopCoroutine();
            descriptionText.text = "";
            examineButton.interactable = true;
        }
        else 
        {
            ResetSelection();
        }
    }

    public void ResetSelection()
    {
        useButton.interactable = false;
        equipButton.interactable = false;
        reloadButton.interactable = false;
        nameText.text = "SELECT AN ITEM";
        FindObjectOfType<CoroutineHelper>().HelperStopCoroutine();
        descriptionText.text = "";
        examineButton.interactable = false;
        mapDisplayer.Exit();
    }

    public void ViewMap(Sprite map) 
    {
        mapDisplayer.GetComponent<MapDisplayer>().ViewMap(map);
    }

    public bool GetIsOn() 
    {
        return isOn;
    }

    public GameObject GetViewedItem() 
    { 
        return viewedItem; 
    }

    public void Use() 
    {
        viewedItem.GetComponent<IItem>().Use();
    }

    public void Examine() 
    {
        viewedItem.GetComponent<IItem>().Examine();
    }

    public void Equip() 
    {
        viewedItem.GetComponent<IItem>().Equip();
    }

    public void Reload() 
    {
        viewedItem.GetComponent<IItem>().Reload();
    }
}
