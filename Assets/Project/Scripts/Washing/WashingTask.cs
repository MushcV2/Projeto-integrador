using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WashingTask : ObjectsInteract
{
    [SerializeField] private Transform lockPlayer;
    public bool isWashing;

    public override void InteractFunction()
    {
        if (!isWashing)
        {
            isWashing = true;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            crossHair.gameObject.SetActive(false);

            if (player.isCrouching) player.Crounch();

            player.transform.position = lockPlayer.position;
            playerInteract.gameObject.GetComponent<PlayerController>().clockUI.GetComponent<Animator>().SetTrigger("Close");
            player.canMove = false;

            cam.GetComponent<CameraController>().characterHead.localPosition = new Vector3(0f, cam.GetComponent<CameraController>().initialPos, 0f);
            cam.GetComponent<CameraController>().stopFollowing = true;
            cam.transform.eulerAngles = new Vector3(0f, 90, 0f);
        }
    }

    public override void StopInteract()
    {
        if (isWashing)
        {
            isWashing = false;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            crossHair.gameObject.SetActive(true);

            player.canMove = true;
            cam.GetComponent<CameraController>().stopFollowing = false;
        }
    }
}