using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookingSystem : ObjectsInteract
{
    public Transform[] slots;

    public override void InteractFunction()
    {
        Debug.Log("Interagiu com o fogao");
    }

    public override void StopInteract()
    {
        Debug.Log("Parou de interagir");
    }
}