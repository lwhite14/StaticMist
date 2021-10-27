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
    float cooldownCounter;

    void Start()
    {
        cooldownCounter = 0f;
    }

    void Update()
    {
        cooldownCounter -= Time.deltaTime;
        if (cooldownCounter <= 0)
        {
            SetIsAppeared(false);
        }
        else 
        {
            SetIsAppeared(true);
        }
    }

    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
    }

    public void SetIsAppeared(bool isAppear)
    {
        anim.SetBool("isAppeared", isAppear);
    }

    public void ChangeValueDeplete(float value)
    {
        slider.value = value;
        cooldownCounter = cooldown;
    }

    public void ChangeValueAdd(float value)
    {
        slider.value = value;
    }
}
