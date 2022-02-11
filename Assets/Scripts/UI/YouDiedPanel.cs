using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouDiedPanel : MonoBehaviour
{
    public void YouDied() 
    {
        StatePanel.instance.RestartLevel();
        //Instantiate(diedPanel, GameObject.Find("WinLoseConditionTarget").transform);
    }
}
