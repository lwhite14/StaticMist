using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletePanel : MonoBehaviour
{
    public GameObject endPanelLevel;
    public GameObject endPanelGame;

    public void LevelComplete()
    {
        Instantiate(endPanelLevel, GameObject.Find("WinLoseConditionTarget").transform);
    }

    public void GameComplete()
    {
        Instantiate(endPanelGame, GameObject.Find("WinLoseConditionTarget").transform);
    }
}
