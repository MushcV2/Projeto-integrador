using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ConsumeItem : ObjectsInteract
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && objectCollider.isTrigger && usable)
        {
            playerInteract.alreadyInteract = false;
            popUpUsable.gameObject.SetActive(false);

            task.MissionCompleted(itemIndex);

            Destroy(gameObject);
        }
    }
}