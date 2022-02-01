using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAnimation : MonoBehaviour
{
    public Animator anim;

    public void SetChase() 
    {
        anim.SetBool("isChase", true);
        anim.SetBool("isPatrol", false);
        anim.SetBool("isIdle", false);
        anim.SetBool("isAttack", false);
    }

    public void SetPatrol() 
    {
        anim.SetBool("isChase", false);
        anim.SetBool("isPatrol", true);
        anim.SetBool("isIdle", false);
        anim.SetBool("isAttack", false);
    }

    public void SetIdle() 
    {
        anim.SetBool("isChase", false);
        anim.SetBool("isPatrol", false);
        anim.SetBool("isIdle", true);
        anim.SetBool("isAttack", false);
    }

    public void SetAttack()
    {
        anim.Play("Attack");
    }


}
