using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextCrawl : MonoBehaviour
{
    public float crawlSpeed;
    public RectTransform rectTransform;

    void Start()
    {
        EventSystem.current.SetSelectedGameObject(GameObject.Find("ContinueButton"));
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
        StopAllCoroutines();
        StartCoroutine(ToTop());
    }
}
