using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : ObjectsInteract
{
    [SerializeField] private AudioClip[] doorsSound;
    [SerializeField] private Animator anim;

    protected override void Start()
    {
    }

    public override void InteractFunction()
    {
        if (!canOpen)
        {
            anim.SetTrigger("Interagir");

            playerInteract.alreadyInteract = false;

            audioS.clip = doorsSound[0];
            audioS.Play();

            canOpen = true;

            task.MissionCompleted(itemIndex);

        }
        else
            StopInteract();
    }

    public override void StopInteract()
    {
        anim.SetTrigger("Interagir");

        audioS.clip = doorsSound[1];
        audioS.Play();

        canOpen = false;
        playerInteract.alreadyInteract = false;
    }
}