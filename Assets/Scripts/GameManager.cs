using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            ExitGame();
        }
    }

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
        GameObject.Find("YouDiedPanel").GetComponent<Animator>().SetBool("isDead", true);

        Debug.Log("Kill!!");
        Monster[] monsters = FindObjectsOfType<Monster>();
        foreach (Monster monster in monsters) 
        {
            monster.OnDeath(true);
            monster.StopAllCoroutines();
            monster.StartCoroutine(monster.ReturnToPatrol());
        }
    }
}
