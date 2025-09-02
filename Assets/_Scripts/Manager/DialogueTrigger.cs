using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindAnyObjectByType<DialogueManager>().StartDialogue(dialogue);
    }

    public void CleanDialogue()
    {
        System.Array.Clear(dialogue.sentences, 0, dialogue.sentences.Length);
        System.Array.Resize(ref dialogue.sentences, 0);
    }
}
