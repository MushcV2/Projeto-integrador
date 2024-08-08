using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class UseItem : Item
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && objectCollider.isTrigger && usable)
        {
            playerInteract.alreadyInteract = false;
            usableItem.gameObject.SetActive(false);

            task.MissionCompleted(itemIndex);

            Destroy(gameObject);
        }
    }
}