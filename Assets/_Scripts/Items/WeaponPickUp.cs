using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour, IInteractable
{
    Outline outline;
    DialogueTrigger dialogue;

    public Weapon weapon;
    public string interactText;

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
        PlayerController.instance.AddWeapon(weapon);
        AudioManager.instance.PlaySFX("PickUp");
        gameObject.SetActive(false);
        dialogue.TriggerDialogue();
    }
}