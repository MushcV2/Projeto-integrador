using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ConsumeItem : ObjectsInteract
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && objectCollider.isTrigger && usable)
        {
            audioS.Play();

            playerInteract.alreadyInteract = false;
            popUpUsable.SetActive(false);

            task.MissionCompleted(itemIndex);

            Destroy(gameObject);
        }
    }
}