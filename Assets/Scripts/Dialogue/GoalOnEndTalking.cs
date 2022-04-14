using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalOnEndTalking : MonoBehaviour
{
    public GameObject gameCompleteUIPanel;
    public void Goal()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject monster in monsters)
        {
            Destroy(monster);
        }
        Instantiate(gameCompleteUIPanel, GameObject.Find("WinLoseConditionTarget").transform);     
        if (FindObjectOfType<Viewmodel>() != null)
        {
            Destroy(FindObjectOfType<Viewmodel>().gameObject);
        }
        FindObjectOfType<RunSlider>().SetCanChange(false);
        FindObjectOfType<JumpCoolDownSlider>().SetCanChange(false);
        FindObjectOfType<MouseLook>().SetIsInMenu(true);
        FindObjectOfType<PlayerMovement>().SetIsInMenu(true);
        MusicManager.instance.SwitchToGoal();
        if (GameObject.Find("Crosshair").activeSelf)
        {
            GameObject.Find("Crosshair").SetActive(false);
        }
        DialogueTrigger.StopAllDialogue();
        InventoryUI.canUse = false;

        AnalyticsFunctions.LevelCompleted(GameManager.instance.level);

        GameInformation.instance.Items = new List<IItem>();
        foreach (IItem item in FindObjectOfType<PlayerInventory>().inventory.GetAllItems())
        {
            if (!(item is Map))
            {
                GameInformation.instance.Items.Add(item);
            }
        }
        GameInformation.instance.Health = FindObjectOfType<Health>().GetHealth();
    }
}
