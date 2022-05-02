using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class Bat : MonoBehaviour, IItem
{
    string displayName = "BAT";
    string description = "IF I NEED TO DEFEND MYSELF AGAINST THOSE MONSTERS...";
    bool canUse = false;
    bool canEquip = true;

    public GameObject bat;
    public GameObject rummageSound;
    public GameObject zippingUpSound;

    public void Use() { }

    public void Examine()
    {
        FindObjectOfType<CoroutineHelper>().HelperStopCoroutine();
        FindObjectOfType<CoroutineHelper>().HelperStartExamining(description);
    }

    public void Equip()
    {
        GameObject viewmodel = GameObject.FindWithTag("Viewmodel");
        if (viewmodel == null)
        {
            GameObject tempViewmodelObj = Instantiate(bat, GameObject.Find("ViewmodelTargetPos").transform.position, Quaternion.identity) as GameObject;
            tempViewmodelObj.transform.rotation = GameObject.Find("Main Camera").transform.rotation;
            Instantiate(rummageSound, transform.position, Quaternion.identity);
            FindObjectOfType<CoroutineHelper>().HelperStopCoroutine();
            FindObjectOfType<CoroutineHelper>().HelperStartExamining("MIGHT BE SAFER TO KEEP THIS OUT...");

            AnalyticsFunctions.ItemUtilise("Bat");
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
                FindObjectOfType<CoroutineHelper>().HelperStartExamining("NEED TO PUT AWAY MY " + viewmodel.GetComponent<Viewmodel>().itemName + " FIRST...");
            }
        }
    }

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
}
