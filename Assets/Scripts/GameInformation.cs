using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInformation : MonoBehaviour
{
    public static GameInformation instance = null;

    // Game Properties
    public bool Instructions { get; set; } = true;
    public float Sensitivity { get; set; } = 5.0f;
    public float Health { get; set; } = 4.0f;
    public List<IItem> Items { get; set; } = new List<IItem>();

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

        DontDestroyOnLoad(gameObject);
    }
}