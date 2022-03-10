using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsHandler : MonoBehaviour
{
    MasterControls controls;
    InputAction look;
    InputAction forwardBackward;
    InputAction lateral;

    MouseLook mouseLook;
    PlayerMovement playerMovement;
    PlayerSprinting playerSprinting;
    PlayerCrouching playerCrouching;
    Interact interact;
    InventoryUI inventoryUI;

    void Awake()
    {
        controls = new MasterControls();
    }

    void OnEnable()
    {
        look = controls.Player.Look;
        look.Enable();
        forwardBackward = controls.Player.Walk_ForwardBackwards;
        forwardBackward.Enable();
        lateral = controls.Player.Walk_Laterally;
        lateral.Enable();

        controls.Player.Jump.performed += Jump;
        controls.Player.Jump.Enable();
        controls.Player.Sprint.performed += SprintOn;
        controls.Player.Sprint.canceled += SprintOff;
        controls.Player.Sprint.Enable();
        controls.Player.Crouch.performed += Crouch;
        controls.Player.Crouch.Enable();
        controls.Player.Interact.started += Interact;
        controls.Player.Interact.Enable();
        controls.Player.Action.started += Action;
        controls.Player.Action.Enable();

        controls.UI.Start.performed += Menu;
        controls.UI.Start.Enable();
        controls.UI.Inventory.performed += Inventory;
        controls.UI.Inventory.Enable();
        

    }

    void OnDisable()
    {
        look.Disable();
        forwardBackward.Disable();
        lateral.Disable();
        controls.Player.Sprint.Disable();
        controls.Player.Jump.Disable();
        controls.Player.Crouch.Disable();
        controls.Player.Interact.Disable();

        controls.UI.Start.Disable();
        controls.UI.Inventory.Disable();
    }

    void Jump(InputAction.CallbackContext obj)
    {
        GetComponent<PlayerMovement>().JumpInput();
    }

    void SprintOn(InputAction.CallbackContext obj) 
    {
        GetComponent<PlayerSprinting>().SprintInput(true);
    }

    void SprintOff(InputAction.CallbackContext obj) 
    {
        GetComponent<PlayerSprinting>().SprintInput(false);
    }

    void Crouch(InputAction.CallbackContext obj)
    {
        GetComponent<PlayerCrouching>().CrouchInput();
    }

    void Interact(InputAction.CallbackContext obj) 
    {
        GetComponent<Interact>().InteractInput();
    }

    void Action(InputAction.CallbackContext obj)
    {
        Attack attack = FindObjectOfType<Attack>();
        if (attack != null) 
        {
            attack.Strike();
        }
    }

    void Menu(InputAction.CallbackContext obj)
    {
        FindObjectOfType<SettingsMenu>().SettingsInput();
    }

    void Inventory(InputAction.CallbackContext obj)
    {
        FindObjectOfType<InventoryUI>().InventoryInput();
    }

    void Update()
    {
        GetComponent<PlayerMovement>().MovementSlideX(lateral.ReadValue<float>());
        GetComponent<PlayerMovement>().MovementSlideZ(forwardBackward.ReadValue<float>());
        GetComponentInChildren<MouseLook>().SetMouseX(look.ReadValue<Vector2>().x);
        GetComponentInChildren<MouseLook>().SetMouseY(look.ReadValue<Vector2>().y);
    }

}
