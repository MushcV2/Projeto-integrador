using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWater : MonoBehaviour
{
    [SerializeField] private ParticleSystem waterEffect;
    [SerializeField] private AudioSource waterAudio;
    [SerializeField] private WashingTask washingTask;
    private bool waterIsActive;

    private void Start()
    {
        waterAudio = GetComponent<AudioSource>();

        gameObject.layer = LayerMask.NameToLayer("Default");
        waterEffect.Stop();
    }

    private void Update()
    {
        if (washingTask.isWashing && gameObject.layer != LayerMask.NameToLayer("Interact")) gameObject.layer = LayerMask.NameToLayer("Interact");
        else gameObject.layer = LayerMask.NameToLayer("Default");
    }

    private void OnMouseDown()
    {
        if (!waterIsActive && gameObject.layer == LayerMask.NameToLayer("Interact"))
        {
            waterIsActive = true;
            waterAudio.Play();
            waterEffect.Play();

        }

        else if (waterIsActive && gameObject.layer == LayerMask.NameToLayer("Interact"))
        {
            waterIsActive = false;
            waterAudio.Stop();
            waterEffect.Stop();
        }
    }
}