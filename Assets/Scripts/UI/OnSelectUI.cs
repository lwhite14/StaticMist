using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnSelectUI : MonoBehaviour, ISelectHandler
{
    [SerializeField]
    GameObject selectedSound;

    public void OnSelect(BaseEventData eventData)
    {
        Instantiate(selectedSound, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
