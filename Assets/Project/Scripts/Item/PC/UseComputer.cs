using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        clockPanel.SetActive(false);

        player.canMove = false;
        cam.GetComponent<CameraController>().stopFollowing = true;
    }

    public override void StopInteract()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        crossHair.gameObject.SetActive(true);
        sanityBar.SetActive(true);
        taskPanel.SetActive(true);
        clockPanel.SetActive(true);

        playerInteract.alreadyInteract = true;
        playerInteract.interactObject = chair;

        cam.GetComponent<CameraController>().stopFollowing = false;
    }
}