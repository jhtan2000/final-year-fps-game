using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletPickUp : MonoBehaviour, IInteractable
{
    public string interactText;

    DialogueTrigger dialogue;
    Outline outline;

    void Awake()
    {
        outline = GetComponent<Outline>();
        dialogue = GetComponent<DialogueTrigger>();
    }

    void Update()
    {
        if (Pieces.instance.currentPiece == 4)
        {
            SetLayer();
        }
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
        AudioManager.instance.PlaySFX("PickUp");
        gameObject.SetActive(false);
        dialogue.TriggerDialogue();
        FindAnyObjectByType<DoorToNextScene>().Unlock();
    }

    public void SetLayer()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
}
