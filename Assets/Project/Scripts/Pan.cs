using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : ObjectsInteract
{
    public bool haveFood;
    private Vector3 originalScale;

    protected override void Start()
    {
        base.Start();

        originalScale = transform.localScale;
    }

    private void Update()
    {
        if (playerInteract.interactObject == gameObject)
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out RaycastHit _hit, 100f))
            {
                if (_hit.collider.CompareTag("Oven"))
                {
                    playerInteract.forcePanel = true;

                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        int _rng = Random.Range(0, _hit.collider.gameObject.GetComponent<CookingSystem>().slots.Length);

                        transform.SetParent(_hit.collider.gameObject.GetComponent<CookingSystem>().slots[_rng], true);
                        transform.localPosition = Vector3.zero;

                        playerInteract.forcePanel = false;
                    }
                }
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
        rb.isKinematic = true;
        rb.useGravity = false;

        playerInteract.forcePanel = false;
    }

    public IEnumerator LayerToNormal()
    {
        yield return new WaitForSeconds(0.3f);

        gameObject.layer = LayerMask.NameToLayer("Interact");
    }
}
