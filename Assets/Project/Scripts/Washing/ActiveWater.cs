using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWater : MonoBehaviour
{
    [SerializeField] private GameObject waterCollider;
    [SerializeField] private ParticleSystem waterEffect;
    [SerializeField] private AudioSource waterAudio;
    [SerializeField] private WashingTask washingTask;
    private bool waterIsActive;

    private void Start()
    {
        waterAudio = GetComponent<AudioSource>();

        gameObject.layer = LayerMask.NameToLayer("Default");
        waterEffect.Stop();
        waterCollider.SetActive(false);
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
            waterCollider.SetActive(true);
            waterIsActive = true;
            waterAudio.Play();
            waterEffect.Play();
        }

        else if (waterIsActive && gameObject.layer == LayerMask.NameToLayer("Interact"))
        {
            waterCollider.SetActive(false);
            waterIsActive = false;
            waterAudio.Stop();
            waterEffect.Stop();
        }
    }
}