using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpCoolDownSlider : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public Animator anim;

    public void SetMaxValue(float value) 
    {
        slider.maxValue = value;
    }

    public void SetSliding(bool isSliding)
    {
        anim.SetBool("isSliding", isSliding);
    }

    public void ChangeValue(float value) 
    {
        slider.value = value;
    }

}
