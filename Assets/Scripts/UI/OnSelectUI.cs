using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnSelectUI : MonoBehaviour, ISelectHandler
{
    public static int counter { get; private set; } = 0;
    public GameObject selectedSound { set; get; }

    public void OnSelect(BaseEventData eventData)
    {
        if (counter > 0)
        {
            if (selectedSound != null)
            {
                Instantiate(selectedSound, new Vector3(0, 0, 0), Quaternion.identity);
            }
        }
        else
        {
            counter++;
        }
    }
}
