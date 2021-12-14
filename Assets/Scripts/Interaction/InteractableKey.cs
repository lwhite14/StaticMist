using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableKey : MonoBehaviour, IInteractable
{
    Key key;

    void Start() 
    {
        key = GetComponent<Key>();
    }

    public void Interact() 
    {
        FindObjectOfType<PlayerInventory>().Add(key);
        Destroy(gameObject);
    }
}
