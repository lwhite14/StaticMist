using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePanel : MonoBehaviour
{
    public static StatePanel instance = null;

    //Class Properties
    public Animator animator { get; set; }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        animator = GetComponent<Animator>();
    }

    public void NextLevel()
    {
        animator.Play("NextLevel");
    }

    public void ReturnToMenu()
    {
        animator.Play("ReturnToMenu");
    }

    public void RestartLevel()
    {
        animator.Play("RestartLevel");    
    }

    public void LoadTextCrawl() 
    {
        animator.Play("TextCrawl");
    }

    public void ManagerNextLevel() 
    {
        GameManager.instance.NextLevel();
    } // Function animator triggers.

    public void ManagerReturnToMenu() 
    {
        GameManager.instance.ReturnToMenu();
    } // Function animator triggers.

    public void ManagerRestartLevel()
    {
        GameManager.instance.RestartLevel();
    } // Function animator triggers.

    public void ManagerLoadTextCrawl() 
    {
        GameManager.instance.LoadTextCrawl();
    }
}
