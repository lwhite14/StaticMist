using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StateManager 
{
    public static bool Instructions { get; set; } = true;
    public static float Sensitivity { get; set; } = 5.0f;
    public static List<IItem> Items { get; set; } = new List<IItem>();
}