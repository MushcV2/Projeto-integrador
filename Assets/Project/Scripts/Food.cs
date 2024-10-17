using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : ObjectsInteract
{
    [SerializeField] private bool handlingItem;
    private bool alreadyInPan;
    private GameObject panObject;
    private Vector3 localScale;

    private void Update()
    {
        if (handlingItem && !alreadyInPan)
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(_ray, out RaycastHit _hit, 100);

            if (_hit.collider.gameObject.CompareTag("Pan") && !_hit.collider.gameObject.GetComponent<Pan>().haveFood)
            {
                playerInteract.forcePanel = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    gameObject.layer = LayerMask.NameToLayer("Default");
                    panObject = _hit.collider.gameObject;

                    rb.isKinematic = true;
                    rb.useGravity = false;

                    transform.parent = panObject.transform;
                    transform.localPosition = new Vector3(panObject.transform.position.x, panObject.transform.position.y + 0.2f, panObject.transform.position.z);

                    panObject.GetComponent<Pan>().haveFood = true;
                    panObject.GetComponent<Pan>().food = gameObject;
                    panObject.layer = LayerMask.NameToLayer("Default");

                    StartCoroutine(panObject.GetComponent<Pan>().LayerToNormal());

                    alreadyInPan = true;

                    playerInteract.alreadyInteract = false;
                    playerInteract.forcePanel = false;
                }
            }
            else
                playerInteract.forcePanel = false;
        }
    }

    public override void InteractFunction()
    {
        base.InteractFunction();

        handlingItem = true;
    }

    public override void StopInteract()
    {
        if (panObject != null && panObject.GetComponent<Pan>().haveFood) return;

        gameObject.transform.parent = null;

        rb.isKinematic = false;
        rb.useGravity = true;
        objectCollider.isTrigger = false;

        playerInteract.interactObject = null;
        playerInteract.forcePanel = false;
        playerInteract.alreadyInteract = false;

        handlingItem = false;
        playerInteract.forcePanel = false;
    }
}
