using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouDiedPanel : MonoBehaviour
{
    public GameObject diedPanel;

    public void YouDied() 
    {
        Instantiate(diedPanel, GameObject.Find("WinLoseConditionTarget").transform);
        //FindObjectOfType<GameManager>().RestartLevel();
    }
}
