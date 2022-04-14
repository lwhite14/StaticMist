using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableElectricalGate : MonoBehaviour, IInteractable
{
    public ElectricalGate electricalGate;

    public void Interact() 
    {
        electricalGate.Interactation();
    }
}
