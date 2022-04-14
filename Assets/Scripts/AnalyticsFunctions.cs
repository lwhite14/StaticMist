using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;
using Unity.Services.Analytics;

public static class AnalyticsFunctions 
{
    public static void PlayerEscape(MonsterPathfinding monsterPathfinding) 
    {
        if (!Application.isEditor)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "Monster", monsterPathfinding.monsterInformation.GetName() }
            };
            Events.CustomData("PlayerEscape", parameters);
            Events.Flush();
        }
        else
        {
            Debug.Log("Sending Event: 'PlayerEscape' with: Monster = " + monsterPathfinding.monsterInformation.GetName());
        }
    }

    public static void LevelCompleted(int level)
    {
        if (!Application.isEditor)
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

    public static void Died(string monsterType, int level)
    {
        if (!Application.isEditor)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "Monster", monsterType },
                { "userLevel", level }
            };
            Events.CustomData("Died", parameters);
            Events.Flush();
        }
        else
        {
            Debug.Log("Sending Event: 'Died' with: Monster = " + monsterType + ", and userLevel = " + level.ToString());
        }
    }

    public static void ItemPickUp(string itemType)
    {
        if (!Application.isEditor)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "itemType", itemType },
            };
            Events.CustomData("ItemPickUp", parameters);
            Events.Flush();
        }
        else
        {
            Debug.Log("Sending Event: 'ItemPickUp' with: itemType = " + itemType);
        }
    }

    public static void ItemUtilise(string itemType)
    {
        if (!Application.isEditor)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                { "itemType", itemType },
            };
            Events.CustomData("ItemUtilise", parameters);
            Events.Flush();
        }
        else
        {
            Debug.Log("Sending Event: 'ItemUtilise' with: itemType = " + itemType);
        }
    }
}

