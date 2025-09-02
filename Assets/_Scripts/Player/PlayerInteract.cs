using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract instance;

    [SerializeField] private float reachDistance = 3f;
    [SerializeField] private LayerMask interactLayerMask;
    IInteractable currentInteractable;

    [Header("Interaction")]
    public GameObject interactionUI;
    public TextMeshProUGUI interactionText;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        CheckInteraction();

        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void CheckInteraction()
    {
        Ray ray = Camera.main.ViewportPointToRay(Vector3.one / 2f);
        RaycastHit hit;

        bool hitItem = false;

        if (Physics.Raycast(ray, out hit, reachDistance, interactLayerMask.value)) //hit item
        {
            IInteractable newInteractable = hit.collider.GetComponent<IInteractable>();

            if (currentInteractable != null && newInteractable != currentInteractable)
            {
                currentInteractable.EnableOutline(hitItem);
            }

            if (newInteractable != null)
            {
                hitItem = true;
                SetNewCurrentInteractable(newInteractable);
            }
            else
            {
                DisableCurrentInteractable();
            }
        }
        else //didn't hit item
        {
            DisableCurrentInteractable();
        }

        interactionUI.SetActive(hitItem);
    }

    void SetNewCurrentInteractable(IInteractable newInteractable)
    {
        currentInteractable = newInteractable;
        currentInteractable.EnableOutline(true);
        interactionText.text = currentInteractable.GetInteractionText();
    }

    void DisableCurrentInteractable()
    {
        if (currentInteractable != null)
        {
            currentInteractable.EnableOutline(false);
            currentInteractable = null;
        }
    }
}
