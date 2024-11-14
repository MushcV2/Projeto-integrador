using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class SanityController : MonoBehaviour
{
    [Header("Sanity Variables")]
    [SerializeField] private float maxSanity;
    [SerializeField] private Volume volume;
    [SerializeField] private Vignette vignette;
    [SerializeField] private Sprite[] logoImages;
    public float currentSanity;

    [Header("Sanity UI")]
    [SerializeField] private Slider sanityBar;
    [SerializeField] private Image logoImage;

    protected virtual void Start()
    {
        currentSanity = Random.Range(50, maxSanity);
        sanityBar.maxValue = maxSanity;

        logoImage.sprite = logoImages[0];

        sanityBar.gameObject.SetActive(false);

        if (volume.profile.TryGet<Vignette>(out vignette)) vignette.intensity.value = 0;
    }

    protected virtual void Update()
    {
        sanityBar.value = Mathf.Lerp(sanityBar.value, currentSanity, 0.6f);

        if (currentSanity >= 75)
        {
            logoImage.sprite = logoImages[0];
            vignette.intensity.value = 0f;
        }
        else if (currentSanity < 75 && currentSanity >= 50)
        {
            logoImage.sprite = logoImages[1];
            vignette.intensity.value = 0.1f;
        }
        else if (currentSanity < 50 && currentSanity >= 25)
        {
            logoImage.sprite = logoImages[2];
            vignette.intensity.value = 0.2f;
        }
        else if (currentSanity < 25)
        {
            logoImage.sprite = logoImages[3];
            vignette.intensity.value = 0.3f;
        }
    }

    public void GainSanity(float _sanity)
    {
        sanityBar.gameObject.SetActive(true);
        Invoke(nameof(DisableBar), 3f);

        currentSanity += Mathf.Min(currentSanity + _sanity, maxSanity);
    }

    public void LostSanity(float _sanity)
    {
        sanityBar.gameObject.SetActive(true);
        Invoke(nameof(DisableBar), 3f);

        currentSanity -= Mathf.Max(_sanity, 0);

        if (currentSanity == 0)
            Debug.Log("Sem sanidade");
    }

    private void DisableBar()
    {
        sanityBar.GetComponent<Animator>().SetTrigger("Close");
    }
}
