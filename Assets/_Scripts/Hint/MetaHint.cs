using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetaHint : MonoBehaviour, IInteractable
{
    public GameObject metaScreen;
    public float hintRange;
    public float maxAlpha;
    public string interactText;

    DialogueTrigger dialogue;
    Outline outline;

    void Awake()
    {
        outline = GetComponent<Outline>();
        dialogue = GetComponent<DialogueTrigger>();
    }

    void Start()
    {
        metaScreen.SetActive(false);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        Color color = metaScreen.GetComponent<Image>().color;
        if (distanceToPlayer <= hintRange)
        {
            metaScreen.SetActive(true);
            color.a = maxAlpha - (distanceToPlayer / hintRange);
        }
        else
        {
            metaScreen.SetActive(false);
        }

        if (color.a > maxAlpha)
        {
            color.a = maxAlpha;
        }

        if (color.a < .1f)
        {
            color.a = .1f;
        }

        metaScreen.gameObject.transform.GetComponent<Image>().color = color;
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
        metaScreen.SetActive(false);
        dialogue.TriggerDialogue();
        Pieces.instance.AddHint();
    }
}
