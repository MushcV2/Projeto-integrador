using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CleaningTask : ObjectsInteract
{
    [SerializeField] private Transform[] randomPos;
    [SerializeField] private int stage;
    [SerializeField] private bool already;
    private bool havePlayer;
    private bool canClean;
    private bool finished;
    private GameObject dustObject;

    private void Update()
    {
        if (havePlayer && !finished)
        {
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(_ray, out RaycastHit _hit, 100);

            if (_hit.collider.CompareTag("Dust"))
            {
                if (dustObject == null) dustObject = _hit.collider.gameObject;
                canClean = true;
            }
            else canClean = false;

            if (Input.GetButtonDown("Fire1") && !already && canClean)
            {
                StartCoroutine(Clean());
                player.canMove = false;
            }

            /*
            if (Input.GetButtonDown("Fire1") && already && canClean)
            {
                StopCoroutine(Clean());

                cam.GetComponent<CameraController>().stopFollowing = false;
                already = false;
                player.canMove = true;
            }
            */
        }
    }


    private IEnumerator Clean()
    {
        already = true;

        if (stage == 4)
        {
            cam.GetComponent<CameraController>().stopFollowing = false;

            player.canMove = true;
            finished = true;
            already = false;
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