using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    public Light lightObj;
    bool isOn = false;
    public MeshRenderer lightMesh;
    public Material onMat;
    public Material offMat;
    public int illuminatedMaterialIndex;

    public void Interact() 
    {
        ChangeOnState();
    }

    void ChangeOnState() 
    {
        if (!isOn)
        {
            TurnOn();
        }
        else 
        {
            TurnOff();
        }
    }

    void TurnOn() 
    {
        isOn = true;
        Material[] materials = lightMesh.materials;
        materials[illuminatedMaterialIndex] = onMat;
        lightMesh.materials = materials;

        lightObj.enabled = true;
    }

    void TurnOff() 
    {
        isOn = false;
        Material[] materials = lightMesh.materials;
        materials[illuminatedMaterialIndex] = offMat;
        lightMesh.materials = materials;

        lightObj.enabled = false;
    }
}
