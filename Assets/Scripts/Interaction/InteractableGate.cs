using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGate : MonoBehaviour, IInteractable
{
    public Gate gate;
    public void Interact() 
    {
        gate.Interact();       
    }
}
