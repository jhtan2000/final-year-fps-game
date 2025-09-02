using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour, IInteractable
{
    Outline outline;

    public float healAmount;

    void Awake()
    {
        outline = GetComponent<Outline>();
    }

    public void EnableOutline(bool hit)
    {
        outline.enabled = hit;
    }

    public string GetInteractionText()
    {
        return "Add <color=red>Health<color=red>";
    }

    public void Interact()
    {
        PlayerHealthController.instance.HealPlayer(healAmount);
        gameObject.SetActive(false);
        AudioManager.instance.PlaySFX("PickUp");
    }

}