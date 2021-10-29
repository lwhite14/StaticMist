using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunSlider : MonoBehaviour
{
    public Slider slider;
    public Image fill;
    public Animator anim;

    public float cooldown = 4f;

    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
    }

    void SetIsAppeared(bool isAppear)
    {
        anim.SetBool("isAppeared", isAppear);
    }

    public void ChangeValue(float value)
    {
        slider.value = value;
        SetIsAppeared(true);
        if (value >= slider.maxValue)
        {
            SetIsAppeared(false);
        }
    }
}
