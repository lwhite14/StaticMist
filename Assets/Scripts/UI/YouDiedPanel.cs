using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class YouDiedPanel : MonoBehaviour
{
    public void YouDied() 
    {
        StatePanel.instance.RestartLevel();
    }
}
