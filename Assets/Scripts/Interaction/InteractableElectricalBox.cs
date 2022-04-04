using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableElectricalBox : MonoBehaviour, IInteractable, IKeyInteractable
{
    public ElectricalBox electricalBox;

    public void Interact() 
    {
        electricalBox.Interact();
    }

    public void KeyUse(Key key) 
    {
        electricalBox.CheckIfKey(key);
    }
}