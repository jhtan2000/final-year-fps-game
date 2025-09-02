using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TestInteraction : MonoBehaviour, IInteractable
{
    Outline outline;
    public string[] dialogue;
    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;
    public float textSpeed;
    public float timesBetweenText;
    private int index;

    void Awake()
    {
        outline = GetComponent<Outline>();
    }

    void Start()
    {
        index = 0;
        dialogueText.text = string.Empty;
    }

    void Update()
    {
        if (dialogueText.text == dialogue[index])
        {
            StartCoroutine(WaitForNextLine());
        }
        else
        {
            StopAllCoroutines();
            dialogueText.text = dialogue[index];
        }
    }

    public string GetInteractionText()
    {
        return "Interact !";
    }

    public void Interact()
    {
        StartDialogue();
    }

    public void EnableOutline(bool hit)
    {
        outline.enabled = hit;
    }

    void StartDialogue()
    {
        index = 0;
        dialogueUI.SetActive(true);
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        dialogueText.text = "";
        foreach (char c in dialogue[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return null;
        }
    }

    void NextDialogue()
    {
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogueUI.SetActive(false);
        }
    }

    IEnumerator WaitForNextLine()
    {
        yield return new WaitForSeconds(timesBetweenText);
        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            dialogueUI.SetActive(false);
        }
    }

}
