using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    Animator anim;

    void Start() 
    {
        anim = GetComponent<Animator>();
    }

    public void Strike() 
    {
        anim.Play("BatAttack");
    }
}
