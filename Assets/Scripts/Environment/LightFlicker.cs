using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public bool isFlickering = false;
    public MeshRenderer lightMesh;
    public Material onMat;
    public Material offMat;
    public int illuminatedMaterialIndex;
    public float flickerTime;
    Light pointLight;

    void Start()
    {
        pointLight = GetComponentInChildren<Light>();
        if (isFlickering)
        {
            StartCoroutine(FlickerOff());
        }
    }

    IEnumerator FlickerOn() 
    {
        Material[] materials = lightMesh.materials;
        materials[illuminatedMaterialIndex] = onMat;
        lightMesh.materials = materials;


        pointLight.enabled = true;
        yield return new WaitForSeconds(flickerTime);
        yield return StartCoroutine(FlickerOff());
    }

    IEnumerator FlickerOff() 
    {
        Material[] materials = lightMesh.materials;
        materials[illuminatedMaterialIndex] = offMat;
        lightMesh.materials = materials;

        pointLight.enabled = false;
        yield return new WaitForSeconds(flickerTime);
        yield return StartCoroutine(FlickerOn());
    }

}
