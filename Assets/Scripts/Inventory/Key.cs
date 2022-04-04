using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour, IItem
{
    string displayName = "KEY";
    string description = "I CAN USE THIS TO OPEN A GATE. ";
    bool canUse = true;
    bool canEquip = false;

    public float rayRange = 4f;
    public string code;

    public void Use()
    {
        RaycastHit hitInfo = new RaycastHit();

        Vector3 fwd = GameObject.Find("Main Camera").GetComponent<Camera>().transform.TransformDirection(Vector3.forward);
        bool hit = Physics.Raycast(GameObject.Find("Main Camera").GetComponent<Camera>().transform.position, fwd, out hitInfo, rayRange);

        if (hit)
        {
            GameObject hitObject = hitInfo.transform.gameObject;
            try
            {
                hitObject.GetComponent<IKeyInteractable>().KeyUse(this);
            }
            catch (Exception ex)
            {
                FindObjectOfType<CoroutineHelper>().HelperStopCoroutine();
                FindObjectOfType<CoroutineHelper>().HelperStartExamining("THERE IS NO DOOR FOR ME TO UNLOCK...");
                Debug.Log(ex);
            }
        }
        else 
        {
            FindObjectOfType<CoroutineHelper>().HelperStopCoroutine();
            FindObjectOfType<CoroutineHelper>().HelperStartExamining("THERE IS NO DOOR FOR ME TO UNLOCK...");
        }
    }

    public void Examine()
    {
        FindObjectOfType<CoroutineHelper>().HelperStopCoroutine();
        FindObjectOfType<CoroutineHelper>().HelperStartExamining(description + "THE KEY IS ENGRAVED WITH '" + code + "'...");
    }

    public void Equip() { }


    public GameObject GetInvIcon() 
    {
        return gameObject;
    }

    public bool GetCanUse() 
    {
        return canUse;
    }

    public bool GetCanEquip() 
    { 
        return canEquip; 
    }

    public string GetName() 
    { 
        return displayName; 
    }
    public string GetDescription() 
    {
        return description;
    }

    public void SendDataToAnalytics() 
    {
        if (InitServices.isRecording)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "itemType", "Key" },
            };
            Events.CustomData("ItemUtilise", parameters);
            Events.Flush();
        }
        else
        {
            Debug.Log("Sending Event: 'ItemUtilise' with: itemType = " + "Key");
        }
    }
}
