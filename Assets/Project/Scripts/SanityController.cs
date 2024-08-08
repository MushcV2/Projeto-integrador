using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SanityController : MonoBehaviour
{
    [Header("Sanity Variables")]
    [SerializeField] private float currentSanity;
    [SerializeField] private float maxSanity;

    [Header("Sanity UI")]
    [SerializeField] private Slider sanityBar;

    protected virtual void Start()
    {
        currentSanity = maxSanity;
        sanityBar.maxValue = maxSanity;
    }
    
    protected virtual void Update()
    {
        sanityBar.value = Mathf.Lerp(sanityBar.value, currentSanity, 0.2f);
    }

    public void GainSanity(float _sanity)
    {
        currentSanity += Mathf.Min(currentSanity + _sanity, maxSanity);
    }

    public void LostSanity(float _sanity)
    {
        currentSanity -= Mathf.Max(_sanity, 0);

        if (currentSanity == 0)
            Debug.Log("Sem sanidade");
    }
}
