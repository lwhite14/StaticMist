using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnSelectUI : MonoBehaviour, ISelectHandler
{
    static int counter = 0;
    [SerializeField]
    GameObject selectedSound;

    public void OnSelect(BaseEventData eventData)
    {
        if (counter > 0)
        {
            Instantiate(selectedSound, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else 
        {
            counter++;
        }
    }
}
