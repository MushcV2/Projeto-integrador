using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseComputer : Item
{
    [SerializeField] private GameObject chair;

    protected override void Start()
    {
    }

    public override void InteractFunction()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        crossHair.gameObject.SetActive(false);
        sanityBar.SetActive(false);
        taskPanel.SetActive(false);

        player.canMove = false;
        cam.GetComponent<CameraController>().stopFollowing = true;

        //player.gameObject.transform.position = new Vector3(newPos.position.x, player.gameObject.transform.position.y, newPos.position.z);
    }

    public override void StopInteract()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        crossHair.gameObject.SetActive(true);
        sanityBar.SetActive(true);
        taskPanel.SetActive(true);

        playerInteract.alreadyInteract = true;
        playerInteract.interactObject = chair;

        cam.GetComponent<CameraController>().stopFollowing = false;
    }
}