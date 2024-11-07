using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowerTask : ObjectsInteract
{
    [SerializeField] private GameObject showerPanel;

    protected override void Start()
    {
        base.Start();

        showerPanel.SetActive(false);
    }

    public override void InteractFunction()
    {
        player.canMove = false;

        showerPanel.SetActive(true);
        showerPanel.GetComponent<Animator>().SetTrigger("Open");
        audioS.Play();

        Invoke(nameof(StopShower), 5f);
        Invoke(nameof(ReturnToMove), 6.5f);
    }

    private void StopShower()
    {
        showerPanel.GetComponent<Animator>().SetTrigger("Close");
        audioS.Stop();

        playerInteract.alreadyInteract = false;
        playerInteract.interactObject = null;
    }

    private void ReturnToMove()
    {
        player.canMove = true;
    }
}