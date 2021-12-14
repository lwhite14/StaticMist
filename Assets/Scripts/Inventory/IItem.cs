using UnityEngine;

public interface IItem
{
    void Equip();
    void Reload();
    void Use();
    void Examine();
    GameObject GetInvIcon();
}
