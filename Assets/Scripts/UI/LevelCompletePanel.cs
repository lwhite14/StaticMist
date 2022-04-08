using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelCompletePanel : MonoBehaviour
{
    public void LevelComplete()
    {
        StatePanel.instance.NextLevel();
    }

    public void GameComplete()
    {
        StatePanel.instance.ReturnToMenu();
    }
}
