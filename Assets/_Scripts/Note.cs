using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Note : MonoBehaviour, IInteractable
{


    Outline outline;
    DialogueTrigger dialogue;

    public GameObject noteUI;

    public bool isRead;

    void Awake()
    {
        outline = GetComponent<Outline>();
        dialogue = GetComponent<DialogueTrigger>();
    }

    public string GetInteractionText()
    {
        return "Read";
    }

    public void Interact()
    {
        ReadNote();
    }

    public void EnableOutline(bool hit)
    {
        outline.enabled = hit;
    }

    public void ReadNote()
    {
        if (isRead == true)
        {
            isRead = false;
            noteUI.SetActive(isRead);
            UIController.instance.hud.SetActive(true);
            Time.timeScale = 1f;
            AudioManager.instance.PlaySFX("Paper");
        }
        else
        {
            isRead = true;
            noteUI.SetActive(isRead);
            UIController.instance.hud.SetActive(false);
            Time.timeScale = 0f;
            AudioManager.instance.PlaySFX("Paper");
        }
    }

}
