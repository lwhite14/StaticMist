using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void SetSpeed(float newSpeed) 
    {
        anim.SetFloat("speed", newSpeed);
    }

    public void PlayAttack() 
    {
        anim.Play("Attack");
    }
}
