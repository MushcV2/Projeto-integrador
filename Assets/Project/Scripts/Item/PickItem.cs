using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickItem : Item
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