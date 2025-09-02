using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpatialHint : MonoBehaviour, IInteractable
{
    public GameObject spatialUI;
    public float hintRange;
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
        spatialUI.SetActive(false);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        if (distanceToPlayer <= hintRange)
        {
            spatialUI.SetActive(true);
        }
        else
        {
            spatialUI.SetActive(false);
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
        AudioManager.instance.PlaySFX("Paper");
        gameObject.SetActive(false);
        spatialUI.SetActive(false);
        dialogue.TriggerDialogue();
        Pieces.instance.AddHint();
    }
}
