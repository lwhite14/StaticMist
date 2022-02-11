using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletePanel : MonoBehaviour
{
    public void LevelComplete()
    {
        StatePanel.instance.NextLevel();
        //Instantiate(endPanelLevel, GameObject.Find("WinLoseConditionTarget").transform);
    }

    public void GameComplete()
    {
        StatePanel.instance.FinishGame();
        //Instantiate(endPanelGame, GameObject.Find("WinLoseConditionTarget").transform);
    }
}
