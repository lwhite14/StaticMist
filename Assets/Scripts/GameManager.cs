using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null; 

    public GameObject deathUIPanel;
    public GameObject levelCompleteUIPanel;
    public GameObject gameCompleteUIPanel;
    public GameObject gameInformationObj;

    [Header("Current Level Information")]
    public int level;
    public bool isFirstLevel = false;
    public bool isLastLevel = false;
    public string levelException;

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
    }

    void Start()
    {
        GameInformationSetUp();
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadFirstLevel() 
    {
        StatePanel.instance.NextLevel();
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
        MusicManager.instance.SwitchToGoal();
        FindObjectOfType<InventoryUI>().SetCanUse(false);

        SendDataToAnalytics();

        GameInformation.instance.Items = new List<IItem>();
        foreach (IItem item in FindObjectOfType<PlayerInventory>().inventory.GetAllItems())
        {
            if (!(item is Map))
            {
                GameInformation.instance.Items.Add(item);
            }
        }
        GameInformation.instance.Health = FindObjectOfType<Health>().GetHealth();
        
    }

    public void NextLevel()
    {
        int nextLevel = level + 1;
        string nextLevelName = "Level" + nextLevel;
        SceneManager.LoadScene(nextLevelName, LoadSceneMode.Single);
    }

    void GameInformationSetUp()
    {
        if (FindObjectOfType<GameInformation>() == null)
        {
            Instantiate(gameInformationObj, new Vector3(0, 0, 0), Quaternion.identity);
        }

        if (isFirstLevel)
        {
            GameInformation.instance.Health = 4.0f;
            GameInformation.instance.Items = new List<IItem>();
        }

        ControlsTab controlsTab = FindObjectOfType<ControlsTab>();
        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        Health health = FindObjectOfType<Health>();
        PSX psx = FindObjectOfType<PSX>();
        if (controlsTab != null)
        {
            controlsTab.SetOn(GameInformation.instance.Instructions);
            controlsTab.SetSens(GameInformation.instance.Sensitivity);
        }
        if (playerInventory != null)
        {
            playerInventory.inventory.SetAllItems(GameInformation.instance.Items);
            playerInventory.RefreshUI();
        }
        if (health != null)
        {
            health.InitSetHealth(GameInformation.instance.Health);
        }
        if (psx != null) 
        {
            psx.TurnOnTVUI(GameInformation.instance.TVUI);
        }
    }

    void SendDataToAnalytics() 
    {
        if (InitServices.isRecording)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "userLevel", level }
            };
            Events.CustomData("LevelCompleted", parameters);
            Events.Flush();
        }
        else
        {
            Debug.Log("Sending Event: 'LevelCompleted' with: Level = " + level.ToString());
        }
    }
}
