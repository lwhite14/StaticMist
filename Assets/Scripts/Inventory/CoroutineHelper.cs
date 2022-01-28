using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHelper : MonoBehaviour
{
    public void HelperStartCoroutine(IEnumerator enumerator) 
    {
        StartCoroutine(enumerator);
    }

    public void HelperStopCoroutine(IEnumerator enumerator)
    {
        StopCoroutine(enumerator);
    }
}
// Cannot run coroutines from IItem as it doesn't inherit from monobehaviour, need this class to start the coroutines. 
