using UnityEngine;
using UnityEngine.UI;

public interface IItem
{
    void Use();
    void Examine();
    void Equip();
    GameObject GetInvIcon();
    bool GetCanUse();
    bool GetCanEquip();
    string GetName();
    string GetDescription();
}
