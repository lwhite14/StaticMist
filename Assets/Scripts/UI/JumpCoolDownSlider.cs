using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpCoolDownSlider : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public Animator anim;

    bool canChange = true;

    public void SetMaxValue(float value) 
    {
        slider.maxValue = value;
    }
    // Used to set the max value as it can change in the inspector.

    public void SetSliding(bool isSliding)
    {
        if (canChange)
        {
            anim.SetBool("isSliding", isSliding);
        }
    }
    // Changes animation state.

    public void ChangeValue(float value) 
    {
        if (canChange)
        {
            slider.value = value;
        }
    }
    // Changes raw value. 

    public void SetCanChange(bool newCanChange)
    {
        canChange = newCanChange;
        if (!canChange)
        {
            anim.SetBool("isSliding", false);
        }
    }

}
