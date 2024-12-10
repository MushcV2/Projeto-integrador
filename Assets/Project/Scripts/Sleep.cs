using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleep : ObjectsInteract
{
    [SerializeField] private GameManager gameManager;

    public override void InteractFunction()
    {
        gameManager.DisplayDayPoints();
    }

    public override void StopInteract()
    {
        playerInteract.alreadyInteract = false;
        playerInteract.canInteract = true;
        playerInteract.interactObject = null;
    }
}