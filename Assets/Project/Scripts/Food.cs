using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : ObjectsInteract
{
    [SerializeField] private bool handlingItem;
    private bool alreadyInPan;

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

                    transform.parent = _hit.transform;
                    transform.localPosition = Vector3.zero;

                    _hit.collider.gameObject.GetComponent<Pan>().haveFood = true;
                    _hit.collider.gameObject.layer = LayerMask.NameToLayer("Default");
                    StartCoroutine(_hit.collider.gameObject.GetComponent<Pan>().LayerToNormal());

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
        handlingItem = true;
    }

    public override void StopInteract()
    {
        handlingItem = false;
        playerInteract.forcePanel = false;
    }
}
