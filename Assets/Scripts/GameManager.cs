using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject deathUIPanel;
    public GameObject levelCompleteUIPanel;
    public GameObject gameCompleteUIPanel;

    public int level;
    public bool isLastLevel = false;

    public void ExitGame() 
    {
        Application.Quit();
    }

    public void Restart() 
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void OnDeath() 
    {
        Instantiate(deathUIPanel, GameObject.Find("WinLoseConditionTarget").transform);

        Monster[] monsters = FindObjectsOfType<Monster>();
        foreach (Monster monster in monsters) 
        {
            monster.OnDeath(true);
            monster.StopAllCoroutines();
            monster.StartCoroutine(monster.ReturnToPatrol());
        }
    }

    public void Goal() 
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        foreach (GameObject monster in monsters) 
        {
            Destroy(monster);
        }
        if (!isLastLevel)
        {
            Instantiate(levelCompleteUIPanel, GameObject.Find("WinLoseConditionTarget").transform);
        }
        else 
        {
            Instantiate(gameCompleteUIPanel, GameObject.Find("WinLoseConditionTarget").transform);
        }
        FindObjectOfType<MouseLook>().SetCursorMode(false);
        FindObjectOfType<MouseLook>().SetIsInMenu(true);
        FindObjectOfType<PlayerMovement>().SetIsInMenu(true);
    }

    public void NextLevel() 
    {
        int nextLevel = level + 1;
        string nextLevelName = "Level" + nextLevel;
        Debug.Log(nextLevelName);
        SceneManager.LoadScene(nextLevelName, LoadSceneMode.Single);
    }

    public void GameComplete() 
    {
        string nextLevelName = "Level1";
        SceneManager.LoadScene(nextLevelName, LoadSceneMode.Single);
    }
}
