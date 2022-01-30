using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoroutineHelper : MonoBehaviour
{
    bool runningRoutine = false;

    public void HelperStartExamining(string sentence, Text examineText) 
    {
        if (!runningRoutine)
        {
            StartCoroutine(TypeSentence(sentence, examineText));
        }
    }

    public void HelperStopCoroutine()
    {
        runningRoutine = false;
        StopAllCoroutines();
    }

    IEnumerator TypeSentence(string sentence, Text examineText)
    {
        runningRoutine = true;

        examineText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            examineText.text += letter;
            yield return new WaitForSeconds(0.05f);
        }
        yield return new WaitForSeconds(2.0f);
        examineText.text = "";

        runningRoutine = false;
    }
}
// Cannot run coroutines from IItem as it doesn't inherit from monobehaviour, need this class to start the coroutines. 
