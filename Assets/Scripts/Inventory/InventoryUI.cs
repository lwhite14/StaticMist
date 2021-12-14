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
            isOn = true;
        }
        else 
        {
            anim.SetBool("isOn", false);
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
