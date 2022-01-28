using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompletePanel : MonoBehaviour
{
    public void LevelComplete()
    {
        FindObjectOfType<GameManager>().NextLevel();

    }

    public void GameComplete()
    {
        FindObjectOfType<GameManager>().GameComplete();
    }
}
