using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouDiedPanel : MonoBehaviour
{
    public void YouDied() 
    {
        FindObjectOfType<GameManager>().Restart();
    }
}
