using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveWater : MonoBehaviour
{
    [SerializeField] private GameObject waterEffect;
    [SerializeField] private AudioSource waterAudio;
    private bool waterIsActive;

    private void Start()
    {
        waterAudio = GetComponent<AudioSource>();
        waterEffect.SetActive(false);
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