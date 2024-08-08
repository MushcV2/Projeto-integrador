using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] private Tasks task;
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private float distance;
    public GameObject interactObject;
    public GameObject interactPanel;
    public bool canInteract;
    public bool alreadyInteract;

    private void Start()
    {
        player = GetComponent<PlayerController>();

        interactPanel.SetActive(false);
    }

    private void Update()
    {
        TryInteract();
        Interact();
        ActivePanel();
    }

    private void TryInteract()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        if (Physics.Raycast(_ray, out _hit, distance, interactLayer))
        {
            if (!alreadyInteract)
            {
                canInteract = true;
                interactObject = _hit.collider.gameObject;
            }
        }
        else
            canInteract = false;
    }

    private void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E) && !alreadyInteract && canInteract)
        {
            alreadyInteract = true;
            canInteract = false;

            task.missionObject = interactObject.name;

            if (interactObject != null) interactObject.GetComponent<Item>().InteractFunction();
        }
        else if (Input.GetKeyDown(KeyCode.E) && alreadyInteract || Input.GetKeyDown(KeyCode.E) && player.isSitting)
        {
            alreadyInteract = false;
            task.missionObject = null;

            interactObject.GetComponent<Item>().StopInteract();
        }
    }

    public void ActivePanel()
    {
        if (canInteract && !alreadyInteract)
            interactPanel.SetActive(true);

        else
            interactPanel.SetActive(false);
    }
}