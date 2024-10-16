using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : ObjectsInteract
{
    public bool haveFood;
    private Vector3 originalScale;
    private GameObject ovenObject;
    private bool seeingOven; 

    protected override void Start()
    {
        base.Start();

        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (playerInteract.interactObject == gameObject)
        {
            Debug.Log("Ray cast");

            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out RaycastHit _hit, 100f))
            {
                if (_hit.collider.CompareTag("Oven"))
                {
                    seeingOven = true;
                    playerInteract.forcePanel = true;
                    Debug.Log("Fogao");

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        gameObject.transform.parent = null;
                        ovenObject = _hit.collider.gameObject;

                        Invoke(nameof(ChangePosition), 0.2f);
                    }
                }
                else
                    seeingOven = false;
            }
            else
                playerInteract.forcePanel = false;
        }
    }

    public override void InteractFunction()
    {
        base.InteractFunction();
    }

    public override void StopInteract()
    {
        if (seeingOven) return;

        rb.isKinematic = false;
        rb.useGravity = true;

        playerInteract.interactObject = null;
        playerInteract.forcePanel = false;
        playerInteract.alreadyInteract = false;

        StartCoroutine(LayerToNormal());
    }

    public IEnumerator LayerToNormal()
    {
        yield return new WaitForSeconds(0.3f);

        gameObject.layer = LayerMask.NameToLayer("Interact");
    }

    private void ChangePosition()
    {
        Transform _slotPos = ovenObject.GetComponent<Collider>().gameObject.GetComponent<CookingSystem>().slot.transform;
        transform.position = _slotPos.position;
        transform.eulerAngles = Vector3.zero;

        playerInteract.forcePanel = false;
        playerInteract.interactObject = null;

        gameObject.layer = LayerMask.NameToLayer("Interact");

        Debug.Log("Panela no fogao");
    }
}