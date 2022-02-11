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

    public void FinishGame()
    {
        animator.Play("FinishGame");
    }

    public void RestartLevel()
    {
        animator.Play("RestartLevel");    
    }

    public void ManagerNextLevel() 
    {
        GameManager.instance.NextLevel();
    } // Function animator triggers.

    public void ManagerFinishGame() 
    {
        GameManager.instance.ReturnToMenu();
    } // Function animator triggers.

    public void ManagerRestartLevel()
    {
        GameManager.instance.RestartLevel();
    } // Function animator triggers.
}
