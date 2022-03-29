using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public float onScreenTime = 5.0f;
    public bool showOnce = true;
    bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            if (!isTriggered)
            {
                DialogueManager.instance.EndDialogue();
                PopUp.StopAllPopUps();
                StartCoroutine(Triggered());
            }
        }
    }

    IEnumerator Triggered()
    {
        if (showOnce)
        {
            isTriggered = true;
        }
        GetComponent<DialogueTrigger>().TriggerDialogue();
        yield return new WaitForSeconds(onScreenTime);
        GetComponent<DialogueTrigger>().TriggerNextSentence();
        yield return null;        
    }

    public static void StopAllPopUps() 
    {
        foreach (PopUp popUp in FindObjectsOfType<PopUp>())
        {
            popUp.StopAllCoroutines();
        }
    }
}
