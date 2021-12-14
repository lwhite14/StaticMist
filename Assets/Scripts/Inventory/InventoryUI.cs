using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject[] itemSlots = new GameObject[16];
    Animator anim;
    bool isOn = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void InventoryInput() 
    {
        if (!isOn)
        {
            anim.SetBool("isOn", true);
            FindObjectOfType<MouseLook>().SetCursorMode(false);
            FindObjectOfType<MouseLook>().SetCanMove(false);
            isOn = true;
        }
        else 
        {
            anim.SetBool("isOn", false);
            FindObjectOfType<MouseLook>().SetCursorMode(true);
            FindObjectOfType<MouseLook>().SetCanMove(true);
            isOn = false;
        }
    }

    public void RefreshUI(List<IItem> items) 
    {
        for (int i = 0; i < items.Count; i++)
        {
            itemSlots[i].GetComponent<ItemSlot>().currentInvIcon = items[i].GetInvIcon();
            itemSlots[i].GetComponent<ItemSlot>().Refresh();
        }
    }
}
