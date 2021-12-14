using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableKey : MonoBehaviour, IInteractable
{
    public Key key;

    public void Interact() 
    {
        FindObjectOfType<PlayerInventory>().Add(key);
        Destroy(gameObject);
    }
}
