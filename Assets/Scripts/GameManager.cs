using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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

    GameObject crosshair;

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
        CursorMode();
        if (level == 0) 
        {
            EventSystem.current.SetSelectedGameObject(GameObject.Find("SettingsButton"));
        }
        if (GameObject.Find("Crosshair") != null)
        {
            crosshair = GameObject.Find("Crosshair");
        }
        SettingsMenu.SetStartSettings();
    }

    void Update()
    {
        if (Debug.isDebugBuild)
        {
            if (Input.GetKeyDown(KeyCode.F1) && Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else if (Input.GetKeyDown(KeyCode.F1) && Cursor.lockState == CursorLockMode.None)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
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
        FindObjectOfType<SettingsMenu>().SaveSettings();
        if (FindObjectOfType<ControlsHandler>() != null)
        {
            FindObjectOfType<ControlsHandler>().DeallocateEvents();
        }
        LoadSceneData.sceneToLoad = "Menu";
        SceneManager.LoadScene("Loading");
    }

    public void RestartLevel()    
    {
        FindObjectOfType<SettingsMenu>().SaveSettings();
        if (FindObjectOfType<ControlsHandler>() != null)
        {
            FindObjectOfType<ControlsHandler>().DeallocateEvents();
        }
        if (levelException == null || levelException == "")
        {
            string currentLevelName = "Level" + level;
            LoadSceneData.sceneToLoad = currentLevelName;
            SceneManager.LoadScene("Loading");
        }
        else 
        {
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
        if (FindObjectOfType<Viewmodel>() != null)
        {
            Destroy(FindObjectOfType<Viewmodel>().gameObject);       
        }
        FindObjectOfType<RunSlider>().SetCanChange(false);
        FindObjectOfType<JumpCoolDownSlider>().SetCanChange(false);
        FindObjectOfType<MouseLook>().SetIsInMenu(true);
        FindObjectOfType<PlayerMovement>().SetIsInMenu(true);
        if (crosshair.activeSelf)
        {
            crosshair.SetActive(false);
        }
        DialogueTrigger.StopAllDialogue();
        InventoryUI.canUse = false;
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
        if (FindObjectOfType<Viewmodel>() != null)
        {
            Destroy(FindObjectOfType<Viewmodel>().gameObject);            
        }
        FindObjectOfType<RunSlider>().SetCanChange(false);
        FindObjectOfType<JumpCoolDownSlider>().SetCanChange(false);
        FindObjectOfType<MouseLook>().SetIsInMenu(true);
        FindObjectOfType<PlayerMovement>().SetIsInMenu(true);
        MusicManager.instance.SwitchToGoal();
        if (crosshair.activeSelf)
        {
            crosshair.SetActive(false);
        }
        DialogueTrigger.StopAllDialogue();
        InventoryUI.canUse = false;

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
        if (FindObjectOfType<ControlsHandler>() != null)
        {
            FindObjectOfType<ControlsHandler>().DeallocateEvents();
        }
        int nextLevel = level + 1;
        string nextLevelName = "Level" + nextLevel;
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

    void CursorMode() 
    {
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Cursor.visible == true)
        {
            Cursor.visible = false;
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
