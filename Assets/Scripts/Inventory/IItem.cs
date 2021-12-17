using UnityEngine;

public interface IItem
{
    void Use();
    void Examine();
    void Equip();
    void Reload();
    GameObject GetInvIcon();
    bool GetCanUse();
    bool GetCanEquip();
    bool GetCanReload();
    string GetName();
    string GetDescription();
}
