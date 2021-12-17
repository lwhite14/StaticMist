using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject deathUIPanel;
    public GameObject levelCompleteUIPanel;

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
        Instantiate(levelCompleteUIPanel, GameObject.Find("WinLoseConditionTarget").transform);
        FindObjectOfType<MouseLook>().SetCursorMode(false);
        FindObjectOfType<MouseLook>().SetIsInMenu(true);
        FindObjectOfType<PlayerMovement>().SetIsInMenu(true);
    }

    public void NextLevel() 
    {
        print("loading next level...");
    }

    void GameComplete() 
    {
    
    }
}
