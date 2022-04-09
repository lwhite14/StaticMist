using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCrawl : MonoBehaviour
{
    public float crawlSpeed;
    public float middleTime;
    public RectTransform rectTransform;

    void Start()
    {
        StartCoroutine(BottomToMiddle());
    }

    void Update() 
    {
        //rectTransform.localPosition += Vector3.down * Time.deltaTime;
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
        yield return new WaitForSeconds(middleTime);
        yield return StartCoroutine(MiddleToTop());
    }

    IEnumerator MiddleToTop()
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
}
