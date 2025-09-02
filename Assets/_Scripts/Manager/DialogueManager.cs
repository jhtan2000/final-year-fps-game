using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Dialogue
{
    [TextArea(2, 5)]
    public string[] sentences;
}

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;

    public GameObject dialogueUI;
    public TextMeshProUGUI dialogueText;
    public Queue<string> sentences;
    float autoTextCounter;
    public float timeBetweenLines;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        sentences = new Queue<string>();
    }

    void Update()
    {
        autoTextCounter -= Time.deltaTime;
        if (autoTextCounter < 0)
        {
            DisplayNextSentence();
            autoTextCounter = timeBetweenLines;
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueUI.SetActive(true);
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        autoTextCounter = timeBetweenLines;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char c in sentence.ToCharArray())
        {
            dialogueText.text += c;
            yield return null;
        }
    }

    void EndDialogue()
    {
        dialogueUI.SetActive(false);
        return;
    }
}
