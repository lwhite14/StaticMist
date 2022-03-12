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

    void Update()
    {
        if (Debug.isDebugBuild)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void ExitGame()
    {
        FindObjectOfType<SettingsMenu>().SaveSettings();
        Application.Quit();
    }

    public void LoadFirstLevel() 
    {
        StatePanel.instance.NextLevel();
    }

    public void ReturnToMenu() 
    {
        //SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        FindObjectOfType<SettingsMenu>().SaveSettings();
        FindObjectOfType<ControlsHandler>().DeallocateEvents();
        LoadSceneData.sceneToLoad = "Menu";
        SceneManager.LoadScene("Loading");
    }

    public void RestartLevel()    
    {
        FindObjectOfType<SettingsMenu>().SaveSettings();
        FindObjectOfType<ControlsHandler>().DeallocateEvents();
        if (levelException == null || levelException == "")
        {
            string currentLevelName = "Level" + level;
            //SceneManager.LoadScene(currentLevelName, LoadSceneMode.Single);
            LoadSceneData.sceneToLoad = currentLevelName;
            SceneManager.LoadScene("Loading");
        }
        else 
        {
            //SceneManager.LoadScene(levelException, LoadSceneMode.Single);
            LoadSceneData.sceneToLoad = levelException;
            SceneManager.LoadScene("Loading");
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
        FindObjectOfType<SettingsMenu>().SaveSettings();
        FindObjectOfType<ControlsHandler>().DeallocateEvents();
        int nextLevel = level + 1;
        string nextLevelName = "Level" + nextLevel;
        //SceneManager.LoadScene(nextLevelName, LoadSceneMode.Single);
        LoadSceneData.sceneToLoad = nextLevelName;
        SceneManager.LoadScene("Loading");
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

        PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();
        Health health = FindObjectOfType<Health>();
        if (playerInventory != null)
        {
            playerInventory.inventory.SetAllItems(GameInformation.instance.Items);
            playerInventory.RefreshUI();
        }
        if (health != null)
        {
            health.InitSetHealth(GameInformation.instance.Health);
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
