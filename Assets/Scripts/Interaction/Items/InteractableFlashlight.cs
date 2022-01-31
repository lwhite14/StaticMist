using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableFlashlight : MonoBehaviour, IInteractable
{
    public Flashlight flashlight;
    public GameObject pickUpSound;

    public void Interact()
    {
        FindObjectOfType<PlayerInventory>().Add(flashlight);
        PickUpSound();
        Destroy(gameObject);
    }

    void PickUpSound()
    {
        Instantiate(pickUpSound, transform.position, Quaternion.identity);
    }
}
