﻿using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class Candle : MonoBehaviour, IItem
{
    string displayName = "CANDLE";
    string description = "WILL HELP ME SEE... CASTS A SHORTER, BUT WIDER LIGHT.";
    bool canUse = false;
    bool canEquip = true;
    bool canReload = false;

    public GameObject candle;
    public GameObject rummageSound;
    public GameObject zippingUpSound;

    public void Use() { }

    public void Examine()
    {
        FindObjectOfType<CoroutineHelper>().HelperStartExamining(description);
    }

    public void Equip()
    {
        GameObject viewmodel = GameObject.FindWithTag("Viewmodel");
        if (viewmodel == null)
        {
            Instantiate(candle, GameObject.Find("ViewmodelTargetPos").transform.position, Quaternion.identity);
            Instantiate(rummageSound, transform.position, Quaternion.identity);
            FindObjectOfType<CoroutineHelper>().HelperStopCoroutine();
            FindObjectOfType<CoroutineHelper>().HelperStartExamining("IT WOULD BE BETTER IF I COULD SEE...");

            SendDataToAnalytics();
        }
        else
        {
            if (viewmodel.GetComponent<Viewmodel>().itemName == displayName)
            {
                Destroy(GameObject.FindWithTag("Viewmodel"));
                Instantiate(zippingUpSound, transform.position, Quaternion.identity);
                FindObjectOfType<CoroutineHelper>().HelperStopCoroutine();
                FindObjectOfType<CoroutineHelper>().HelperStartExamining("I BEST PUT THIS AWAY...");
            }
            else
            {
                FindObjectOfType<CoroutineHelper>().HelperStopCoroutine();
                FindObjectOfType<CoroutineHelper>().HelperStartExamining("NEED TO PUT AWAY MY " + viewmodel.GetComponent<Viewmodel>().itemName + " FIRST ...");
            }
        }
    }

    public void Reload() { }

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

    public bool GetCanReload()
    {
        return canReload;
    }

    public string GetName()
    {
        return displayName;
    }

    public string GetDescription()
    {
        return description;
    }

    void SendDataToAnalytics()
    {
        if (InitServices.isRecording)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "itemType", "Candle" },
            };
            Events.CustomData("ItemUtilise", parameters);
            Events.Flush();
        }
        else
        {
            Debug.Log("Sending Event: 'ItemUtilise' with: itemType = " + "Flashlight");
        }
    }
}