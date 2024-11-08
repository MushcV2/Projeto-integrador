using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UseComputer : ObjectsInteract
{
    [SerializeField] private GameObject chair;
    [SerializeField] private GameManager gameManager;
    public bool onPc;

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

        playerInteract.gameObject.GetComponent<PlayerController>().clockUI.GetComponent<Animator>().SetTrigger("Close");
        player.canMove = false;
        onPc = true;
        gameManager.multiplier = 2f;

        cam.GetComponent<CameraController>().characterHead.localPosition = new Vector3(0f, cam.GetComponent<CameraController>().initialPos, 0f);
        cam.GetComponent<CameraController>().stopFollowing = true;
        StartCoroutine(UpdateRotCam(0.7f));
    }

    private IEnumerator UpdateRotCam(float _duration)
    {
        Quaternion _startRot = cam.transform.rotation;
        float _timeElapse = 0;

        while (_timeElapse < _duration)
        {
            cam.transform.rotation = Quaternion.Slerp(_startRot, new Quaternion(0, 0, 0, 1), _timeElapse / _duration);
            _timeElapse += Time.deltaTime;

            yield return null;
        }

        cam.transform.rotation = new Quaternion(0, 0, 0, 1);
    }

    public override void StopInteract()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        crossHair.gameObject.SetActive(true);
        taskPanel.SetActive(true);

        gameManager.multiplier = 1f;
        playerInteract.alreadyInteract = true;
        playerInteract.interactObject = chair;
        onPc = false;

        cam.GetComponent<CameraController>().stopFollowing = false;
    }
}