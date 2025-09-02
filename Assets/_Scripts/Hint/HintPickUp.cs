using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintPickUp : MonoBehaviour, IInteractable
{

    public string interactText;

    DialogueTrigger dialogue;
    Outline outline;

    void Awake()
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
        return interactText;
    }

    public void Interact()
    {
        AudioManager.instance.PlaySFX("Paper");
        gameObject.SetActive(false);
        dialogue.TriggerDialogue();
        Pieces.instance.AddHint();
    }
}