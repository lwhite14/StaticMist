using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    Animator anim;
    MonsterPathfinding monsterPathfinding;

    void Start()
    {
        anim = GetComponent<Animator>();
        monsterPathfinding = GetComponent<MonsterPathfinding>();
    }

    void Update()
    {
        SetSpeed(monsterPathfinding.GetSpeed());
    }

    void SetSpeed(float newSpeed) 
    {
        anim.SetFloat("speed", newSpeed);
    }
}
