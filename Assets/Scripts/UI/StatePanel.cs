using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePanel : MonoBehaviour
{
    public void NextLevel()
    {
        FindObjectOfType<GameManager>().NextLevel();
    }

    public void FinishGame()
    {
        FindObjectOfType<GameManager>().ReturnToMenu();
    }

    public void RestartLevel()
    {
        FindObjectOfType<GameManager>().RestartLevel();
    }
}
