using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitOnChair : ObjectsInteract
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

        cam.GetComponent<CameraController>().characterHead.localPosition = new Vector3(0f, cam.GetComponent<CameraController>().initialPos, 0f);
        player.gameObject.transform.position = new Vector3(newPos.position.x, player.gameObject.transform.position.y, newPos.position.z);
        playerInteract.gameObject.GetComponent<PlayerController>().clockUI.GetComponent<Animator>().SetTrigger("Close");

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