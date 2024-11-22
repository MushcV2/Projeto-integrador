using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CleaningTask : ObjectsInteract
{
    [SerializeField] private Transform[] randomPos;
    [SerializeField] private int stage;
    [SerializeField] private bool already;
    private GameObject dustObject;
    private bool havePlayer;
    private bool canClean;
    private bool finished;
    private bool startedCleaning;

    private void Update()
    {
        if (havePlayer && !finished) VerifyDust();
    }


    private IEnumerator Clean()
    {
        already = true;
        startedCleaning = true;

        if (!audioS.isPlaying) audioS.Play();

        if (stage >= 4)
        {
            cam.GetComponent<CameraController>().stopFollowing = false;
            audioS.Stop();

            player.canMove = true;
            finished = true;
            already = false;

            popUpUsable.SetActive(false);
            dustObject.SetActive(false);

            task.MissionCompleted(itemIndex);

            yield break;
        }

        player.canMove = false;
        cam.GetComponent<CameraController>().characterHead.localPosition = new Vector3(0f, cam.GetComponent<CameraController>().initialPos, 0f);
        cam.GetComponent<CameraController>().stopFollowing = true;

        Debug.Log("Mudou de estagio");

        stage++;
        yield return new WaitForSeconds(1.2f);

        StartCoroutine(Clean());
    }

    public void ResetMission()
    {
        if (dustObject != null)
        {
            already = false;
            finished = false;

            dustObject.SetActive(true);
            dustObject.transform.position = randomPos[Random.Range(0, randomPos.Length)].position;
            stage = 0;
        }
    }

    private void VerifyDust()
    {
        Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(_ray, out RaycastHit _hit, 3);

        if (_hit.collider.CompareTag("Dust"))
        {
            if (dustObject == null) dustObject = _hit.collider.gameObject;
            if (!startedCleaning) popUpUsable.SetActive(true);

            canClean = true;
        }
        else
        {
            if (!startedCleaning) popUpUsable.SetActive(false);
            canClean = false;
        }

        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.F) && !already && canClean)
        {
            StartCoroutine(Clean());
            player.canMove = false;
        }
    }

    public override void InteractFunction()
    {
        base.InteractFunction();

        havePlayer = true;
    }

    public override void StopInteract()
    {
        base.StopInteract();

        havePlayer = false;
    }
}