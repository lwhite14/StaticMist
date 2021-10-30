using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    public Animator anim;

    public void SetIsRunning(bool isRunning)
    {
        anim.SetBool("isRunning", isRunning);
    }   

}
