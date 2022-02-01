using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject deathUIPanel;
    public GameObject levelCompleteUIPanel;
    public GameObject gameCompleteUIPanel;

    [Header("Current Level Information")]
    public int level;
    public bool isLastLevel = false;
    public string levelException;

    void Start()
    {
        SetUp();
    }
    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadFirstLevel() 
    {
        SceneManager.LoadScene("Level1", LoadSceneMode.Single);
    }

    public void RestartGame() 
    {
        LoadFirstLevel();
    }

    public void ReturnToMenu() 
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }

    public void RestartLevel()    
    {
        if (levelException == null || levelException == "")
        {
            string currentLevelName = "Level" + level;
            SceneManager.LoadScene(currentLevelName, LoadSceneMode.Single);
        }
        else 
        {
            SceneManager.LoadScene(levelException, LoadSceneMode.Single);
        }
    }

    public void OnDeath()
    {
        Instantiate(deathUIPanel, GameObject.Find("WinLoseConditionTarget").transform);

        MonsterPathfinding[] monsters = FindObjectsOfType<MonsterPathfinding>();
        foreach (MonsterPathfinding monster in monsters)
        {
            monster.OnDeath(true);
            monster.StopAllCoroutines();
            monster.StartCoroutine(monster.ReturnToPatrol());
        }
        FindObjectOfType<InventoryUI>().SetCanUse(false);
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
        FindObjectOfType<MusicManager>().SwitchToGoal();
        FindObjectOfType<InventoryUI>().SetCanUse(false);

        StateManager.Items = new List<IItem>();
        foreach (IItem item in FindObjectOfType<PlayerInventory>().inventory.GetAllItems()) 
        {
            StateManager.Items.Add(item);
        }
    }

    public void NextLevel()
    {
        int nextLevel = level + 1;
        string nextLevelName = "Level" + nextLevel;
        SceneManager.LoadScene(nextLevelName, LoadSceneMode.Single);
    }

    void SetUp() 
    {
        ControlsTab controlsTab = FindObjectOfType<ControlsTab>();
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        if (controlsTab != null)
        {
            controlsTab.SetOn(StateManager.Instructions);
            controlsTab.SetSens(StateManager.Sensitivity);
        }
        if (playerInventory != null)
        {
            playerInventory.inventory.SetAllItems(StateManager.Items);
            playerInventory.RefreshUI();
        }
    }
}
