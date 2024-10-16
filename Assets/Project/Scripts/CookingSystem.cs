using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingSystem : ObjectsInteract
{
    public Transform slot;
    public GameObject food;
    public bool haveFood;
    private bool isActive;

    private void Update()
    {
        if (isActive && haveFood)
            Debug.Log("Cozinhando");
    }

    public override void InteractFunction()
    {
        if (!isActive) isActive = true;
        else isActive = false;

        gameObject.layer = LayerMask.NameToLayer("Default");
        StopInteract();
    }

    public override void StopInteract()
    {
        playerInteract.alreadyInteract = false;
        Invoke(nameof(Cooldown), 1);
    }

    private void Cooldown()
    {
        gameObject.layer = LayerMask.NameToLayer("Interact");
    }
}