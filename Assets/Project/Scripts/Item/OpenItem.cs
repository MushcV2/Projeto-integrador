using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenItem : Item
{
    [SerializeField] private Animator anim;

    protected override void Start()
    {
    }

    public override void InteractFunction()
    {
        if (!canOpen)
        {
            anim.SetTrigger("OpenDoor");

            playerInteract.alreadyInteract = false;
            canOpen = true;

            task.MissionCompleted(itemIndex);

        }
        else
            StopInteract();
    }

    public override void StopInteract()
    {
        anim.SetTrigger("CloseDoor");

        canOpen = false;
        playerInteract.alreadyInteract = false;
    }
}