using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGate : MonoBehaviour, IInteractable, IKeyInteractable
{
    public Gate gate;
    public void Interact() 
    {
        gate.Interact();       
    }

    public void KeyUse(Key key) 
    {
        gate.CheckIfKey(key);
    }
}
