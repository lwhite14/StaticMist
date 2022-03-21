using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class YouDiedPanel : MonoBehaviour
{
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(transform.GetChild(0).gameObject);
    }

    public void YouDied() 
    {
        StatePanel.instance.RestartLevel();
        //Instantiate(diedPanel, GameObject.Find("WinLoseConditionTarget").transform);
    }
}
