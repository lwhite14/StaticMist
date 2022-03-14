using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public float onScreenTime = 5.0f;
    bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            DialogueManager.instance.EndDialogue();
            foreach (PopUp popUp in FindObjectsOfType<PopUp>())
            {
                popUp.StopAllCoroutines();
            }
            StartCoroutine(Triggered());
        }
    }

    IEnumerator Triggered()
    {
        if (!isTriggered)
        {
            isTriggered = true;
            GetComponent<DialogueTrigger>().TriggerDialogue();
            yield return new WaitForSeconds(onScreenTime);
            GetComponent<DialogueTrigger>().TriggerNextSentence();
            yield return null;
        }
    }
}
