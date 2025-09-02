using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowDialogue : MonoBehaviour, IInteractable
{
    DialogueTrigger dialogue;
    Outline outline;

    public string text;

    private void Awake()
    {
        outline = GetComponent<Outline>();
        dialogue = GetComponent<DialogueTrigger>();
    }

    public void EnableOutline(bool hit)
    {
        outline.enabled = hit;
    }

    public string GetInteractionText()
    {
        return text;
    }

    public void Interact()
    {
        dialogue.TriggerDialogue();
    }
}
