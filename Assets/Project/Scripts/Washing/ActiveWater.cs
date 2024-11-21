using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWater : MonoBehaviour
{
    [SerializeField] private GameObject waterEffect;
    [SerializeField] private AudioSource waterAudio;
    [SerializeField] private WashingTask washingTask;
    private bool waterIsActive;

    private void Start()
    {
        waterAudio = GetComponent<AudioSource>();
        waterEffect.SetActive(false);

        gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void Update()
    {
        if (washingTask.isWashing && gameObject.layer != LayerMask.NameToLayer("Interact")) gameObject.layer = LayerMask.NameToLayer("Interact");
        //else gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void OnMouseDown()
    {
        if (!waterIsActive)
        {
            waterIsActive = true;
            waterAudio.Play();

            waterEffect.SetActive(true);
        }

        else
        {
            waterIsActive = false;
            waterAudio.Stop();

            waterEffect.SetActive(false);
        }
    }
}