using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControlsHandler : MonoBehaviour
{
    MasterControls controls;
    InputAction look;
    InputAction forwardBackward;
    InputAction lateral;

    void OnEnable()
    {
        controls = InputManager.inputActions;
        AssignEvents();
    }

    void AssignEvents() 
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
        DeallocateEvents();
    }

    public void DeallocateEvents() 
    {
        look.Disable();
        forwardBackward.Disable();
        lateral.Disable();

        controls.Player.Jump.performed -= Jump;
        controls.Player.Jump.Disable();
        controls.Player.Sprint.performed -= SprintOn;
        controls.Player.Sprint.canceled -= SprintOff;
        controls.Player.Sprint.Disable();
        controls.Player.Crouch.performed -= Crouch;
        controls.Player.Crouch.Disable();
        controls.Player.Interact.started -= Interact;
        controls.Player.Interact.Disable();
        controls.Player.Action.started -= Action;
        controls.Player.Action.Disable();

        controls.UI.Start.performed -= Menu;
        controls.UI.Start.Disable();
        controls.UI.Inventory.performed -= Inventory;
        controls.UI.Inventory.Disable();
    }

    void Jump(InputAction.CallbackContext obj)
    {
        if (GetComponent<PlayerMovement>() != null)
        {
            if (!SettingsMenu.paused)
            {
                GetComponent<PlayerMovement>().JumpInput();
            }
        }
    }

    void SprintOn(InputAction.CallbackContext obj) 
    {
        if (GetComponent<PlayerSprinting>() != null)
        {
            if (!SettingsMenu.paused)
            {
                GetComponent<PlayerSprinting>().SprintInput(true);
            }
        }
    }

    void SprintOff(InputAction.CallbackContext obj)
    {
        if (GetComponent<PlayerSprinting>() != null)
        {
            if (!SettingsMenu.paused)
            {
                GetComponent<PlayerSprinting>().SprintInput(false);
            }
        }
    }

    void Crouch(InputAction.CallbackContext obj)
    {
        if (GetComponent<PlayerCrouching>() != null)
        {
            if (!SettingsMenu.paused)
            {
                GetComponent<PlayerCrouching>().CrouchInput();
            }
        }
    }

    void Interact(InputAction.CallbackContext obj) 
    {
        if (GetComponent<Interact>() != null)
        {
            if (!SettingsMenu.paused)
            {
                GetComponent<Interact>().InteractInput();
            }
        }
    }

    void Action(InputAction.CallbackContext obj)
    {
        if (FindObjectOfType<Attack>() != null) 
        {
            if (!SettingsMenu.paused)
            {
                FindObjectOfType<Attack>().Strike();
            }
        }
    }

    void Menu(InputAction.CallbackContext obj)
    {
        if (FindObjectOfType<SettingsMenu>() != null)
        {
            FindObjectOfType<SettingsMenu>().SettingsInput();
        }
    }

    void Inventory(InputAction.CallbackContext obj)
    {
        if (FindObjectOfType<InventoryUI>() != null)
        {
            if (!SettingsMenu.paused)
            {
                FindObjectOfType<InventoryUI>().InventoryInput();
            }
        }
    }

    void Update()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        MouseLook mouseLook = FindObjectOfType<MouseLook>();

        playerMovement.MovementSlideX(lateral.ReadValue<float>());
        playerMovement.MovementSlideZ(forwardBackward.ReadValue<float>());
        mouseLook.SetMouseX(look.ReadValue<Vector2>().x);
        mouseLook.SetMouseY(look.ReadValue<Vector2>().y);
    }
}
