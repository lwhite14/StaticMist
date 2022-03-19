using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryUI : MonoBehaviour
{
    public static bool canUse { get; set; } = true;
    public static bool isOn { get; set; } = false;

    public GameObject[] itemSlots = new GameObject[16];
    public Button useButton;
    public Button examineButton;
    public Button equipButton;
    public Text nameText;
    public Text descriptionText;
    public MapDisplayer mapDisplayer;
    GameObject viewedItem = null;
    Animator anim;

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

                foreach (Selectable selectableUI in Selectable.allSelectablesArray)
                {
                    if (selectableUI.gameObject.tag == "Inventory")
                    {
                        selectableUI.interactable = true;
                    }
                }

                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(GameObject.Find("ItemSpot1").transform.GetChild(0).gameObject);
            }
            else
            {
                anim.SetBool("isOn", false);
                FindObjectOfType<MouseLook>().SetCursorMode(true);
                FindObjectOfType<MouseLook>().SetIsInMenu(false);
                FindObjectOfType<PlayerMovement>().SetIsInMenu(false);
                isOn = false;

                foreach (Selectable selectableUI in Selectable.allSelectablesArray)
                {
                    if (selectableUI.gameObject.tag == "Inventory") 
                    {
                        selectableUI.interactable = false;
                    }
                }

                EventSystem.current.SetSelectedGameObject(null);
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

    public void SetViewedItem(GameObject newViewedItem) 
    {
        viewedItem = newViewedItem;
        if (viewedItem != null)
        {
            useButton.interactable = viewedItem.GetComponent<IItem>().GetCanUse();
            equipButton.interactable = viewedItem.GetComponent<IItem>().GetCanEquip();
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
}
