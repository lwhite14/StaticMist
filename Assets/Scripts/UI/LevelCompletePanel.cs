using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelCompletePanel : MonoBehaviour
{
    void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(transform.GetChild(0).gameObject);
    }
    public void LevelComplete()
    {
        StatePanel.instance.NextLevel();
        //Instantiate(endPanelLevel, GameObject.Find("WinLoseConditionTarget").transform);
    }

    public void GameComplete()
    {
        StatePanel.instance.ReturnToMenu();
        //Instantiate(endPanelGame, GameObject.Find("WinLoseConditionTarget").transform);
    }
}
