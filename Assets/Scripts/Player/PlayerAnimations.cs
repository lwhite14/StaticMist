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
    // Seperated animations into a different script so the other player scripts are less convoluted.
}
