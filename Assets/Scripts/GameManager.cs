using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
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
        print("Reached the end!");
    }
}
