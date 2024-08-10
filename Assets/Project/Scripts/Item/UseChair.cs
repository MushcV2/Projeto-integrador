using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseChair : Item
{
    [SerializeField] private Transform newPos;
    [SerializeField] private GameObject computer;

    protected override void Start()
    {
        computer.layer = LayerMask.NameToLayer("Default");
    }

    public override void InteractFunction()
    {
        if (player.isCrouching)
        {
            playerInteract.alreadyInteract = false;
            return;
        }

        player.canMove = false;
        player.isSitting = true;
        playerInteract.alreadyInteract = false;

        player.gameObject.transform.position = new Vector3(newPos.position.x, player.gameObject.transform.position.y, newPos.position.z);

        computer.layer = LayerMask.NameToLayer("Interact");
        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    public override void StopInteract()
    {
        player.canMove = true;
        player.isSitting = false;

        player.gameObject.transform.position += new Vector3(2, 0, 0);

        computer.layer = LayerMask.NameToLayer("Default");
        gameObject.layer = LayerMask.NameToLayer("Interact");
    }
}