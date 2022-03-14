using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            DialogueManager.instance.EndDialogue();
            StopAllCoroutines();
            StartCoroutine(Triggered());
        }
    }

    IEnumerator Triggered()
    {
        if (!isTriggered)
        {
            isTriggered = true;
            GetComponent<DialogueTrigger>().TriggerDialogue();
            yield return new WaitForSeconds(5.0f);
            GetComponent<DialogueTrigger>().TriggerNextSentence();
            yield return null;
        }
    }
}
