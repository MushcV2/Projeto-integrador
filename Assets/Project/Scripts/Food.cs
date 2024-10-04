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

            Debug.Log(_hit.collider.gameObject.tag);

            if (_hit.collider.gameObject.CompareTag("Pan") && !_hit.collider.gameObject.GetComponent<Pan>().haveFood)
            {
                playerInteract.forcePanel = true;

                if (Input.GetKeyDown(KeyCode.E))
                {
                    transform.parent = _hit.transform;
                    transform.localPosition = Vector3.zero;

                    Debug.Log("Nova position");

                    _hit.collider.gameObject.GetComponent<Pan>().haveFood = true;
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
    }
}
