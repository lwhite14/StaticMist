using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextCrawl : MonoBehaviour
{
    public float crawlSpeed;
    public RectTransform rectTransform;
    public GameObject continueButton;
    Animator anim;

    void Start()
    {
        anim = GameObject.Find("ContinueButton").GetComponent<Animator>();

        StartCoroutine(BottomToMiddle());
    }

    IEnumerator BottomToMiddle() 
    {
        while (rectTransform.localPosition.y < 0)
        {
            rectTransform.localPosition += Vector3.up * Time.deltaTime * crawlSpeed;
            yield return null;
        }
        yield return StartCoroutine(StayInMiddle());
    }

    IEnumerator StayInMiddle()
    {
        EventSystem.current.SetSelectedGameObject(continueButton);
        anim.SetBool("isAppear", true);
        rectTransform.localPosition = new Vector3(0, 0, 0);
        yield return null;
    }

    IEnumerator ToTop()
    {
        while (rectTransform.localPosition.y < 1000)
        {
            rectTransform.localPosition += Vector3.up * Time.deltaTime * crawlSpeed;
            yield return null;
        }
        Continue();
        yield return null;
    }

    void Continue() 
    {
        StatePanel.instance.NextLevel();
    }

    public void ContinueCrawl() 
    {
        anim.SetBool("isAppear", false);
        StopAllCoroutines();
        StartCoroutine(ToTop());
    }
}
