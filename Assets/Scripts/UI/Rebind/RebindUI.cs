using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RebindUI : MonoBehaviour
{
    [SerializeField]
    InputActionReference inputActionReference;

    [SerializeField]
    bool excludeMouse = true;
    [Range(0, 10)]
    [SerializeField]
    int selectedBinding;
    [SerializeField]
    InputBinding.DisplayStringOptions displayStringOptions;
    [Header("Binding Info - DO NOT EDIT")]
    [SerializeField]
    InputBinding inputBinding;
    int bindingIndex;

    string actionName;

    [Header("UI Fields")]
    [SerializeField]
    Text actionText;
    [SerializeField]
    Button rebindButton;
    [SerializeField]
    Text rebindText;
    [SerializeField]
    Button resetButton;

    void OnEnable()
    {
        rebindButton.onClick.AddListener(() => DoRebind());
        resetButton.onClick.AddListener(() => ResetBinding());

        if (inputActionReference != null) 
        {
            GetBindingInfo();
            UpdateUI();
        }

        InputManager.rebindComplete += UpdateUI;
    }

    void OnDisable()
    {
        InputManager.rebindComplete -= UpdateUI;
    }

    void OnValidate()
    {
        if (inputActionReference == null) 
        {
            return;
        }
        GetBindingInfo();
        UpdateUI();
    }

    void GetBindingInfo() 
    {
        if (inputActionReference.action != null) 
        {
            actionName = inputActionReference.action.name;
        }

        if (inputActionReference.action.bindings.Count > selectedBinding) 
        {
            inputBinding = inputActionReference.action.bindings[selectedBinding];
            bindingIndex = selectedBinding;
        }
    }

    void UpdateUI() 
    {
        if (actionText != null) 
        {
            actionText.text = actionName;
        }
        if (rebindText != null) 
        {
            if (Application.isPlaying)
            {
                rebindText.text = InputManager.GetBindingName(actionName, bindingIndex);
            }
            else 
            {
                rebindText.text = inputActionReference.action.GetBindingDisplayString(bindingIndex);
            }
        }
    }

    void DoRebind() 
    {
        InputManager.StartRebind(actionName, bindingIndex, rebindText);
    }

    void ResetBinding() 
    {
    
    }
}
