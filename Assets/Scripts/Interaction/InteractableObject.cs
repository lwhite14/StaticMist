using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractable
{
    public bool hasInteracted { private set; get; } = false;

    public void Interact()
    {
        hasInteracted = true;
    }
}

// Script made to test the Interact() function.