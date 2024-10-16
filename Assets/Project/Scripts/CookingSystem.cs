using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingSystem : ObjectsInteract
{
    public Transform slot;

    public override void InteractFunction()
    {
        StopInteract();
    }

    public override void StopInteract()
    {

    }
}