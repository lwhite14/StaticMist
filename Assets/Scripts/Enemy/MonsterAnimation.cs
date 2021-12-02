using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    Animator anim;

    void Start() 
    {
        anim = GetComponentInChildren<Animator>();
    }

    public void SetChase() 
    {
        anim.SetBool("isChase", true);
        anim.SetBool("isPatrol", false);
        anim.SetBool("isIdle", false);
    }

    public void SetPatrol() 
    {
        anim.SetBool("isChase", false);
        anim.SetBool("isPatrol", true);
        anim.SetBool("isIdle", false);
    }

    public void SetIdle() 
    {
        anim.SetBool("isChase", false);
        anim.SetBool("isPatrol", false);
        anim.SetBool("isIdle", true);
    }
    

}
