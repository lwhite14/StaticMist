using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour, IItem
{
    bool canUse = true;
    bool canEquip = false;
    bool canReload = false;

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

    public GameObject GetInvIcon() 
    {
        return gameObject;
    }

}
